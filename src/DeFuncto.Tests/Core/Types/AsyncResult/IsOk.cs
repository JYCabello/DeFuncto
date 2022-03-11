using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncResult;

public class IsOk
{
    [Property(DisplayName = "Confirms if it's Ok")]
    public void OnError(string a) =>
        _ = Error<int, string>(a)
            .Async()
            .IsOk
            .Run(Assert.False)
            .Result;

    [Property(DisplayName = "Confirms if it's not Ok")]
    public void OnOk(string a) =>
        _ = Ok<string, int>(a)
            .Async()
            .IsOk
            .Run(Assert.True)
            .Result;
}
