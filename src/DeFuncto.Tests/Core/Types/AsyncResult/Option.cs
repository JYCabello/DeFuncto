using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncResult;

public class Option
{
    [Property(DisplayName = "Maps Ok to Some")]
    public void OkToSome(NonNull<string> a) =>
        _ = Ok(a.Get)
            .Result<int>()
            .Async()
            .Option
            .ShouldBeSome(a.Get)
            .Result;

    [Property(DisplayName = "Maps Error to None")]
    public void ErrorToNone(NonNull<string> a) =>
        _ = Error(a.Get)
            .Result<int>()
            .Async()
            .Option
            .ShouldBeNone()
            .Result;
}
