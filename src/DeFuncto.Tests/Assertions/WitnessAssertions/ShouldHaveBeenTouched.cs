using DeFuncto.Assertions;
using Xunit;

namespace DeFuncto.Tests.Assertions.WitnessAssertions;

public class ShouldHaveBeenTouched
{
    [Fact(DisplayName = "Fails if not touched")]
    public void Fails()
    {
        var witness = new Witness();
        Assert.Throws<AssertionFailedException>(witness.ShouldHaveBeenTouched);
    }

    [Fact(DisplayName = "Fails if not touched less than required times")]
    public void FailsTimes()
    {
        var witness = new Witness();
        witness.Touch();
        witness.ShouldHaveBeenTouched(1);
        Assert.Throws<AssertionFailedException>(() => witness.ShouldHaveBeenTouched(2));
    }
}
