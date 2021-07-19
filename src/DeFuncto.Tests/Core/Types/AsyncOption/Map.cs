using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class Map
    {
        [Property(DisplayName = "Maps Some")]
        public void OnSome(NonNull<string> a, string b) =>
            Some(a)
            .Async()
            .Map(val => val + b)                
            .ShouldBeSome(a + b);

        [Fact(DisplayName = "Maps Some async")]
        public Task OnSomeAsync() =>
            Some("ban")
                .Async()
                .Map(val => $"{val}ana".ToTask())
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Skips on None")]
        public Task OnNone() =>
            None.Option<Task<string>>().Async()
                .Map(_ => "banana")
                .ShouldBeNone();

        [Fact(DisplayName = "Skips on None async")]
        public Task OnNoneAsync() =>
            None.Option<Task<string>>().Async()
                .Map(_ => "banana".ToTask())
                .ShouldBeNone();
    }
}
