using System.Threading.Tasks;
using DeFuncto.Assertions;
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
    }
}
