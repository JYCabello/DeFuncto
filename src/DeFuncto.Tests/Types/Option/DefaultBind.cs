using DeFuncto.Assertions;
using Xunit;

using static DeFuncto.Prelude;
namespace DeFuncto.Tests.Types.Option
{
    public class DefaultBind
    {
        [Fact(DisplayName = "Skips bind on Some by value")]
        public void SomeValue() =>
            Some("banana")
                .DefaultBind(Some("pear"))
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Skips bind on Some by lambda")]
        public void SomeLambda() =>
            Some("banana")
                .DefaultBind(() => Some("pear"))
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Binds on None by value")]
        public void NoneValue() =>
            None.Option<string>()
                .DefaultBind(Some("banana"))
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Binds on None by lambda")]
        public void NoneLambda() =>
            None.Option<string>()
                .DefaultBind(() => Some("banana"))
                .ShouldBeSome("banana");
    }
}
