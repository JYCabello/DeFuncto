using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Result
    {
        [Fact(DisplayName = "Converts some into success result")]
        public void SomeToSuccess() =>
            Optional("take a deep breath and relax")
                .Result(42)
                .ShouldBeOk("take a deep breath and relax");

        [Fact(DisplayName = "Converts none into error result")]
        public void NoneToEror() =>
            Optional((int?) null)
                .Result("banana")
                .ShouldBeError("banana");
    }
}
