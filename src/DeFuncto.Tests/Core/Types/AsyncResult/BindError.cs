using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncResult
{
    public class BindError
    {
        [Property(DisplayName = "Binds on both Error with a sync bind")]
        public void BothError(string a, string b) =>
            Error<string, string>(a)
                .Async()
                .BindError(val => Error<string, string>(val + b))
                .ShouldBeError(a + b);

        [Property(DisplayName = "Gives second Ok on first error with a sync bind")]
        public void FirstErrorSecondNot(NonNull<string> a, NonNull<string> b) =>
            _ = Error<string, string>(a.Get)
                .Async()
                .BindError(_ => Ok<string, string>(b.Get).Async())
                .ShouldBeOk(b.Get)
                .Result;

        [Property(DisplayName = "Gives first error on second Ok with task")]
        public void FirstErrorSecondOk(NonNull<string> a, int ok) =>
            _ = Error<int, string>(a.ToString())
                .Async()
                .BindError(_ => Ok<int, string>(ok).ToTask())
                .ShouldBeOk(ok)
                .Result;

        [Property(DisplayName = "Gives first error on second error with task")]
        public void FirstErrorSecondAlso(NonNull<string> a, NonNull<string> b) =>
            _ = Error<int, string>(a.Get)
                .Async()
                .BindError(_ => Error<int, string>(b.Get).ToTask())
                .ShouldBeError(b.Get)
                .Result;

        [Property(DisplayName = "Keeps being OK while trying to bind an error in a task")]
        public void FirstOkSecondTaskError(NonNull<string> a) =>
            _ = Ok<string, int>(a.Get)
                .Async()
                .BindError(_ => Error<string, int>(42).ToTask())
                .ShouldBeOk(a.Get)
                .Result;
    }
}
