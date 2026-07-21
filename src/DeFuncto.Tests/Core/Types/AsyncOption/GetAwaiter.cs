using System.Threading.Tasks;
using DeFuncto.Assertions;

namespace DeFuncto.Tests.Core.Types.AsyncOption;

public class GetAwaiter
{
    [Fact(DisplayName = "Awaits some")]
    public async Task AwaitsSome()
    {
        var opt = await Some(1).Async();
        opt.ShouldBeSome(n =>
        {
            Assert.Equal(1, n);
            return unit;
        });
    }

    [Fact(DisplayName = "Awaits none")]
    public async Task AwaitsNone()
    {
        var opt = await None.Option<int>().Async();
        opt.ShouldBeNone();
    }
}
