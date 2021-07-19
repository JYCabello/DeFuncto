using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck.Xunit;
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

        [Property(DisplayName = "Skips with an option")]
        public void SomeOption(string a, string b) =>
            Some(a).Async()
                .BindNone(Some(b))
                .ShouldBeSome(a);

        [Property(DisplayName = "Binds with an option task")]
        public void SomeTaskOption(string a, string b) =>
            Some(a).Async()
                .BindNone(Some(b).ToTask())
                .ShouldBeSome(a);

        [Property(DisplayName = "Binds with an async option")]
        public void SomeAsyncOption(string a, string b) =>
            Some(a).Async()
                .BindNone(Some(b).Async())
                .ShouldBeSome(a);
    }
}
