using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Bind
    {
        [Fact(DisplayName = "Binds two somes")]
        public void SomeOnSome() =>
            Some(42)
                .Bind(number => number == 42 ? Some("banana") : Some("pear"))
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Binds none after some")]
        public void NoneOnSome() =>
            Some("banana")
                .Bind(_ => None.Option<int>())
                .ShouldBeNone();

        [Fact(DisplayName = "Skips none after none")]
        public void NoneOnNone() =>
            None.Option<string>()
                .Bind(_ => None.Option<int>())
                .ShouldBeNone();

        [Fact(DisplayName = "Skip some after none")]
        public void SomeOnNone() =>
            None.Option<int>()
                .Bind(number => number == 42 ? Some("banana") : Some("pear"))
                .ShouldBeNone();
    }
}
