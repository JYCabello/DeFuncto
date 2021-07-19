using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class BindError
    {
        [Property(DisplayName = "Binds on both Error")]
        public void BothError(NonNull<string> a, NonNull<string> b) =>
            Error<string, string>(a.Get)
                .BindError(val => Error<string, string>($"{val}{b.Get}"))
                .ShouldBeError($"{a.Get}{b.Get}");

        [Property(DisplayName = "Gives second Ok on first error")]
        public void FirstErrorSecondNot(NonNull<string> a, NonNull<string> b) =>
            Error<string, string>(a.Get)
                .BindError(_ => Ok<string, string>(b.Get))
                .ShouldBeOk(b.Get);

        [Property(DisplayName = "Skips on first Ok with second error")]
        public void FirstOkSecondError(NonNull<string> a, int b) =>
            Ok<string, int>(a.Get)
                .BindError(_ => Error<string, int>(b))
                .ShouldBeOk(a.Get);

        [Property(DisplayName = "Skips on first Ok with second Ok")]
        public void FirstOkSecondOk(NonNull<string> a, NonNull<string> b) =>
            Ok<string, int>(a.Get)
                .BindError(_ => Ok<string, int>(b.Get))
                .ShouldBeOk(a.Get);
    }
}
