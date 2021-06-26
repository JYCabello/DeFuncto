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

        [Fact(DisplayName = "Maps with an asynchronous projection")]
        public Task Asnchronous() =>
            Ok<string, int>("ban")
                .Async()
                .Map(val => $"{val}ana".Apply(Task.FromResult))
                .ShouldBeOk("banana");
    }
}
