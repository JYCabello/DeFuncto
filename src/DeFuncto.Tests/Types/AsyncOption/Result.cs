using System.Threading.Tasks;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncOption
{
    public class Result
    {
        [Fact(DisplayName = "Maps Some to Ok")]
        public Task SomeToOk() =>
            Some("banana")
                .Async()
                .Result(42)
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Maps None to Error")]
        public Task NoneToError() =>
            None.Option<int>()
                .Async()
                .Result("banana")
                .ShouldBeError("banana");
    }
}
