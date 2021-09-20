using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncResult
{
    public class Map
    {
        [Property(DisplayName = "Maps with a synchronous projection")]
        public void Synchronous(string a, string b) =>
           _ = Ok<string, int>(a)
                .Async()
                .Map(val => $"{val}{b}")
                .ShouldBeOk(a + b)
                .Result;

        [Property(DisplayName = "Skips with a synchronous projection")]
        public void SyncError(NonNull<string> a, NonNull<string> b) =>
           _ = Error<int, string>(a.Get)
                .Async()
                .Map(_ => b.Get)
                .ShouldBeError(a.Get)
                .Result;

        [Property(DisplayName = "Maps with an asynchronous projection")]
        public void Asnchronous(NonNull<string> a, NonNull<string> b) =>
            _ = Ok<string, int>(a.Get)
                .Async()
                .Map(val => $"{val}{b.Get}".ToTask())
                .ShouldBeOk(a.Get + b.Get)
                .Result;

        [Property(DisplayName = "Skips with an asynchronous projection")]
        public void AsyncError(NonNull<string> a, NonNull<string> b) =>
            _ = Error<int, string>(a.Get)
                .Async()
                .Map(_ => b.Get.ToTask())
                .ShouldBeError(a.Get)
                .Result;

    }
}
