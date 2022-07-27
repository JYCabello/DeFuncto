using System.Threading.Tasks;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption;

public class GetAwaiter
{
    [Fact(DisplayName = "Awaits some")]
    public async Task AwaitsSome()
    {
        Option<int> opt = await Some(1).Async();
        opt.ShouldBeSome(n =>
        {
            Assert.Equal(1, n);
            return unit;
        });
    }
}
