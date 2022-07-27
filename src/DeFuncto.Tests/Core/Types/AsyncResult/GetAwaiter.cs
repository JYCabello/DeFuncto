﻿using System.Threading.Tasks;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncResult;

public class GetAwaiter
{
    [Fact(DisplayName = "Awaits Ok")]
    public async Task AwaitsOk()
    {
        var opt = await Ok(1).Result<int>().Async();
        opt.ShouldBeOk(n =>
        {
            Assert.Equal(1, n);
            return unit;
        });
    }

    [Fact(DisplayName = "Awaits Error")]
    public async Task AwaitsError()
    {
        var opt = await Error(1).Result<int>().Async();
        opt.ShouldBeError(n =>
        {
            Assert.Equal(1, n);
            return unit;
        });
    }
}
