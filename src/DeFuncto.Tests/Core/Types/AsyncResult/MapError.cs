using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncResult;

public class MapError
{
    [Property(DisplayName = "Skips with a synchronous projection")]
    public void Synchronous(NonNull<string> a) =>
        _ = Ok<string, int>(a.Get)
            .Async()
            .MapError(_ => 42)
            .ShouldBeOk(a.Get)
            .Result;

    [Property(DisplayName = "Maps with a synchronous projection")]
    public void SyncError(NonNull<string> a, NonNull<string> b) =>
        _ = Error<int, string>(a.Get)
            .Async()
            .MapError(val => $"{val}{b.Get}")
            .ShouldBeError(a.Get + b.Get)
            .Result;

    [Property(DisplayName = "Skips with an asynchronous projection")]
    public void Asnchronous(NonNull<string> a) =>
        _ = Ok<string, int>(a.Get)
            .Async()
            .MapError(_ => 42.ToTask())
            .ShouldBeOk(a.Get)
            .Result;

    [Property(DisplayName = "Maps with an asynchronous projection")]
    public void AsyncError(NonNull<string> a, NonNull<string> b) =>
        _ = Error<int, string>(a.Get)
            .Async()
            .MapError(val => $"{val}{b.Get}".ToTask())
            .ShouldBeError(a.Get + b.Get)
            .Result;
}
