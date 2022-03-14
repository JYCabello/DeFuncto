using System.Threading.Tasks;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Assertions.OptionAssertions;

public class ShouldBeSome
{
    [Fact(DisplayName = "Should fail if is none")]
    public void FailNone()
    {
        Option<int> none = None;
        Assert.Throws<AssertionFailedException>(() => none.ShouldBeSome());
    }

    [Fact(DisplayName = "Should fail if is different some")]
    public void FailDifferentNone()
    {
        var some = Some(1);
        Assert.Throws<AssertionFailedException>(() => some.ShouldBeSome(2));
    }

    [Fact(DisplayName = "Should fail if is different some async")]
    public async Task FailDifferentNoneAsyncc()
    {
        var some = Some(1).Async();
        await Assert.ThrowsAsync<AssertionFailedException>(() => some.ShouldBeSome(2));
    }
}
