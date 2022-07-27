using System.Threading.Tasks;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncResult;

public class GetAwaiter
{
    [Fact(DisplayName = "Awaits Ok")]
    public async Task AwaitsOk()
    {
        var result = await Ok(1).Result<int>().Async();
        result.ShouldBeOk(n =>
        {
            Assert.Equal(1, n);
            return unit;
        });
    }

    [Fact(DisplayName = "Awaits Error")]
    public async Task AwaitsError()
    {
        var result = await Error(1).Result<int>().Async();
        result.ShouldBeError(n =>
        {
            Assert.Equal(1, n);
            return unit;
        });
    }
}
