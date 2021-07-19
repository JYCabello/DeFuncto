using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class BindNone
    {
        [Property(DisplayName = "Binds with an option")]
        public void NoneOption(NonNull<string> a) =>
            _ = None.Option<string>().Async()
                .BindNone(Some(a.Get))
                .ShouldBeSome(a.Get)
                .Result;

        [Property(DisplayName = "Binds with an option task")]
        public void NoneTaskOption(NonNull<string> a) =>
            _ = None.Option<string>().Async()
                .BindNone(Some(a.Get).ToTask())
                .ShouldBeSome(a.Get)
                .Result;

        [Property(DisplayName = "Binds with an async option")]
        public void NoneAsyncOption(NonNull<string> a) =>
            _ = None.Option<string>().Async()
                .BindNone(Some(a.Get).Async())
                .ShouldBeSome(a.Get)
                .Result;

        [Property(DisplayName = "Skips with an option")]
        public void SomeOption(NonNull<string> a, NonNull<string> b) =>
            _ = Some(a.Get).Async()
                .BindNone(Some(b.Get))
                .ShouldBeSome(a.Get)
                .Result;

        [Property(DisplayName = "Binds with an option task")]
        public void SomeTaskOption(NonNull<string> a, NonNull<string> b) =>
            _ = Some(a.Get).Async()
                .BindNone(Some(b.Get).ToTask())
                .ShouldBeSome(a.Get)
                .Result;

        [Property(DisplayName = "Binds with an async option")]
        public void SomeAsyncOption(NonNull<string> a, NonNull<string> b) =>
            _ = Some(a.Get).Async()
                .BindNone(Some(b.Get).Async())
                .ShouldBeSome(a.Get)
                .Result;
    }
}
