using System.Threading.Tasks;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncOption
{
    public class Map
    {
        [Fact(DisplayName = "Maps Some")]
        public Task OnSome() =>
            Some("ban")
                .Async()
                .Map(val => $"{val}ana")
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Skips on None async")]
        public Task OnNone() =>
            None.Option<Task<string>>().Async()
                .Map(_ => Task.FromResult("banana"))
                .ShouldBeNone();
    }
}
