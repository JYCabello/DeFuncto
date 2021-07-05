using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class MapError
    {

        [Fact(DisplayName = "Skips with a synchronous projection")]
        public Task Synchronous() =>
            Ok<string, int>("banana")
                .Async()
                .MapError(_ => 42)
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Maps with a synchronous projection")]
        public Task SyncError() =>
            Error<int, string>("ban")
                .Async()
                .MapError(val => $"{val}ana")
                .ShouldBeError("banana");

        [Fact(DisplayName = "Skips with an asynchronous projection")]
        public Task Asnchronous() =>
            Ok<string, int>("banana")
                .Async()
                .MapError(_ => 42.Apply(Task.FromResult))
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Maps with an asynchronous projection")]
        public Task AsyncError() =>
            Error<int, string>("ban")
                .Async()
                .MapError(val => $"{val}ana".Apply(Task.FromResult))
                .ShouldBeError("banana");
    }
}
