using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class Option
    {
        [Fact(DisplayName = "Turns Ok into Some")]
        public void OkToSome() =>
            Ok<string, int>("banana")
                .Option
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Turns Error into None")]
        public void ErrorToNone() =>
            Error<int, string>("banana")
                .Option
                .ShouldBeNone();
    }
}
