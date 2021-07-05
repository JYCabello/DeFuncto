using System.Threading.Tasks;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class Option
    {
        [Fact(DisplayName = "Maps Ok to Some")]
        public Task OkToSome() =>
            Ok("banana")
                .Result<int>()
                .Async()
                .Option
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Maps Error to None")]
        public Task ErrorToNone() =>
            Error("banana")
                .Result<int>()
                .Async()
                .Option
                .ShouldBeNone();
    }
}
