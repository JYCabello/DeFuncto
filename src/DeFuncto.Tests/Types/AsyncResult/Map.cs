using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class Map
    {
        [Fact(DisplayName = "Maps with a synchronous projection")]
        public Task Synchronous() =>
            Ok<string, int>("ban")
                .Async()
                .Map(val => $"{val}ana")
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Skips with a synchronous projection")]
        public Task SyncError() =>
            Error<int, string>("banana")
                .Async()
                .Map(_ => "pear")
                .ShouldBeError("banana");

        [Fact(DisplayName = "Maps with an asynchronous projection")]
        public Task Asnchronous() =>
            Ok<string, int>("ban")
                .Async()
                .Map(val => $"{val}ana".ToTask())
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Skips with an asynchronous projection")]
        public Task AsyncError() =>
            Error<int, string>("banana")
                .Async()
                .Map(_ => "pear".ToTask())
                .ShouldBeError("banana");

    }
}
