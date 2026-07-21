using DeFuncto.Assertions;

namespace DeFuncto.Tests.Assertions.OptionAssertions;

public class ShouldBeNone
{
    [Fact(DisplayName = "Fails if it's some")]
    public void FailsSome()
    {
        var some = Some(1);
        Assert.Throws<AssertionFailedException>(() => some.ShouldBeNone());
    }
}
