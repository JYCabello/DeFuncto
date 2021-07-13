using System;
using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class Bind
    {
        [Property(DisplayName = "Binds on both Ok")]
        public void BothOk(string a, string b) =>
            Ok<string, int>(a)
                .Bind(val => Result<string, int>.Ok($"{val}{b}"))
                .ShouldBeOk($"{a}{b}");

        [Property(DisplayName = "Gives second error on first Ok")]
        public void FirstOkSecondNot(NonNull<string> a, NonNull<string> b) =>
            Ok<string, string>(a.Get)
                .Bind(_ => Error<int, string>(b.Get))
                .ShouldBeError(b.Get);

        [Property(DisplayName = "Gives first error on second Ok")]
        public void FirstErrorSecondOk(NonNull<string> a, int b) =>
            Error<int, string>(a.Get)
                .Bind(_ => Result<int, string>.Ok(b))
                .ShouldBeError(a.Get);

        [Property(DisplayName = "Gives first error on second error")]
        public void FirstErrorSecondAlso(NonNull<string> a, NonNull<string> b) =>
            Error<int, string>(a.Get)
                .Bind(_ => Result<int, string>.Error(b.Get))
                .ShouldBeError(a.Get);

        [Property(DisplayName = "Does not run the binder on first error")]
        public void FirstErrorNoBinderRun(NonNull<string> a) =>
            Error<int, string>(a.Get)
                .Bind<int>(_ => throw new Exception("Should not run"))
                .ShouldBeError(a.Get);
    }
}
