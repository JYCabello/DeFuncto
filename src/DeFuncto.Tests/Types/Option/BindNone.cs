using DeFuncto.Assertions;
using Xunit;

using static DeFuncto.Prelude;
namespace DeFuncto.Tests.Types.Option
{
    public class BindNone
    {
        [Fact(DisplayName = "Skips bind on Some by value")]
        public void SomeValue() =>
            Some("banana")
                .BindNone(Some("pear"))
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Skips bind on Some by lambda")]
        public void SomeLambda() =>
            Some("banana")
                .BindNone(() => Some("pear"))
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Binds on None by value")]
        public void NoneValue() =>
            None.Option<string>()
                .BindNone(Some("banana"))
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Binds on None by lambda")]
        public void NoneLambda() =>
            None.Option<string>()
                .BindNone(() => Some("banana"))
                .ShouldBeSome("banana");
    }
}
