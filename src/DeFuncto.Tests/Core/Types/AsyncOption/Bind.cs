using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class Bind
    {
        [Fact(DisplayName = "Binds two somes")]
        public void SomeOnSome() =>
            Some(42)
                .Async()
                .Bind(number => number == 42 ? Some("banana") : Some("pear"))
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Binds none after some")]
        public void NoneOnSome() =>
            Some("banana")
                .Async()
                .Bind(_ => None.Option<int>().ToTask())
                .ShouldBeNone();

        [Fact(DisplayName = "Skips none after none")]
        public void NoneOnNone() =>
            None.Option<string>()
                .Async()
                .Bind(_ => None.Option<int>())
                .ShouldBeNone();

        [Fact(DisplayName = "Skips some after none")]
        public void SomeOnNone() =>
            None.Option<int>()
                .Async()
                .Bind(number => number == 42 ? Some("banana") : Some("pear"))
                .ShouldBeNone();
    }
}
