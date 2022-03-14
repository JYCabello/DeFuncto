using DeFuncto.Assertions;
using Xunit;

namespace DeFuncto.Tests.Assertions.ConcurrentWitness;

public class ShouldBeenHeldMax
{
    [Fact(DisplayName = "Fails when a different number of concurrent holds is asserted")]
    public void Fails()
    {
        var witness = new DeFuncto.Assertions.ConcurrentWitness();
        using (var _ = witness.Grab())
        using (var __ = witness.Grab()) { }

        using (var _ = witness.Grab())
        using (var __ = witness.Grab()) { }

        witness.ShouldBeenHeldMax(2);
        Assert.Throws<AssertionFailedException>(() => witness.ShouldBeenHeldTotal(1));
        Assert.Throws<AssertionFailedException>(() => witness.ShouldBeenHeldTotal(3));
    }
}
