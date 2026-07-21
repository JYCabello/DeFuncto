using DeFuncto.Extensions;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.AsyncResult;

public class Collapse
{
    [Property(DisplayName = "Collapses on OK")]
    public void OnOk(string a) =>
        _ = Ok<string, string>(a)
            .Async()
            .Collapse()
            .Result
            .Run(val => Assert.Equal(a, val));

    [Property(DisplayName = "Collapses on Error")]
    public void OnError(string a) =>
        _ = Error<string, string>(a)
            .Async()
            .Collapse()
            .Result
            .Run(val => Assert.Equal(a, val));
}
