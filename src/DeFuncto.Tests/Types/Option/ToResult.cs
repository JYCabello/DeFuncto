using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.Option
{
    public class ToResult
    {
        [Fact(DisplayName = "Converts some into success result")]
        public void SomeToSuccess() =>
            Some("banana")
                .ToResult(42)
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Converts none into error result")]
        public void NoneToEror() =>
            None.Option<int>()
                .ToResult("banana")
                .ShouldBeError("banana");
    }
}
