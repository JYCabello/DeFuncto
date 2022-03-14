using DeFuncto.Assertions;
using Xunit;

namespace DeFuncto.Tests.Assertions.ConcurrentWitness;

public class ShouldBeenHeldTotal
{
    [Fact(DisplayName = "Fails when it has been held a different amount of times")]
    public void Fails()
    {
        var witness = new DeFuncto.Assertions.ConcurrentWitness();
        using (var _ = witness.Grab()) { }
        witness.ShouldBeenHeldTotal(1);
        Assert.Throws<AssertionFailedException>(() => witness.ShouldBeenHeldTotal(2));
        Assert.Throws<AssertionFailedException>(() => witness.ShouldBeenHeldTotal(0));
    }
}
