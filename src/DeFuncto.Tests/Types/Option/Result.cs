using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.Option
{
    public class Result
    {
        [Fact(DisplayName = "Converts some into success result")]
        public void SomeToSuccess() =>
            Some("take a deep breath and relax")
                .Result(42)
                .ShouldBeOk("take a deep breath and relax");

        [Fact(DisplayName = "Converts none into error result")]
        public void NoneToEror() =>
            None.Option<int>()
                .Result("banana")
                .ShouldBeError("banana");
    }
}
