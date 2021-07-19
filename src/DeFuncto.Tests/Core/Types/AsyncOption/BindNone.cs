using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class BindNone
    {
        [Property(DisplayName = "Binds with an option")]
        public void NoneOption(string a) =>
            None.Option<string>().Async()
                .BindNone(Some(a))
                .ShouldBeSome(a);

        [Property(DisplayName = "Binds with an option task")]
        public void NoneTaskOption(string a) =>
            None.Option<string>().Async()
                .BindNone(Some(a).ToTask())
                .ShouldBeSome(a);

        [Property(DisplayName = "Binds with an async option")]
        public void NoneAsyncOption(string a) =>
            None.Option<string>().Async()
                .BindNone(Some(a).Async())
                .ShouldBeSome(a);

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
