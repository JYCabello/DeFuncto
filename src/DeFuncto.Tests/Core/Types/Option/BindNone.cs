using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option;

public class BindNone
{
    [Property(DisplayName = "Skips bind on Some by value")]
    public void SomeValue(NonNull<string> a, NonNull<string> b) =>
        Some(a.Get)
            .BindNone(Some(b.Get))
            .ShouldBeSome(a.Get);

    [Property(DisplayName = "Skips bind on Some by lambda")]
    public void SomeLambda(NonNull<string> a, NonNull<string> b) =>
        Some(a.Get)
            .BindNone(() => Some(b.Get))
            .ShouldBeSome(a.Get);

    [Property(DisplayName = "Binds on None by value")]
    public void NoneValue(NonNull<string> a, NonNull<string> b) =>
        None.Option<string>()
            .BindNone(Some(a.Get))
            .ShouldBeSome(a.Get);

    [Property(DisplayName = "Binds on None by lambda")]
    public void NoneLambda(NonNull<string> a, NonNull<string> b) =>
        None.Option<string>()
            .BindNone(() => Some(a.Get))
            .ShouldBeSome(a.Get);
}
