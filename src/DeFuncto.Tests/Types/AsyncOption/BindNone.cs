using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncOption
{
    public class BindNone
    {
        [Fact(DisplayName = "Binds with an option")]
        public void NoneOption() =>
            None.Option<string>().Async()
                .BindNone(Some("banana"))
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Binds with an option task")]
        public void NoneTaskOption() =>
            None.Option<string>().Async()
                .BindNone(Some("banana").ToTask())
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Binds with an async option")]
        public void NoneAsyncOption() =>
            None.Option<string>().Async()
                .BindNone(Some("banana").Async())
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Skips with an option")]
        public void SomeOption() =>
            Some("banana").Async()
                .BindNone(Some("pear"))
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Binds with an option task")]
        public void SomeTaskOption() =>
            Some("banana").Async()
                .BindNone(Some("pear").ToTask())
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Binds with an async option")]
        public void SomeAsyncOption() =>
            Some("banana").Async()
                .BindNone(Some("pear").Async())
                .ShouldBeSome("banana");
    }
}
