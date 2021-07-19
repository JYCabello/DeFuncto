using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;
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
        public void FirstErrorSecondNot(string a, string b) =>
            Error<string, string>(a)
                .Async()
                .BindError(_ => Ok<string, string>(b).Async())
                .ShouldBeOk(b);

        [Property(DisplayName = "Gives first error on second Ok with task")]
        public void FirstErrorSecondOk(string a) =>
            Error<int, string>(a)
                .Async()
                .BindError(_ => Ok<int, string>(42).ToTask())
                .ShouldBeError(a);

        [Fact(DisplayName = "Gives first error on second error with task")]
        public void FirstErrorSecondAlso() =>
            Error<int, string>("banana")
                .Async()
                .BindError(_ => Error<int, string>("pear").ToTask())
                .ShouldBeError("banana");

        [Fact(DisplayName = "Keeps being OK while trying to bind an error in a task")]
        public void FirstOkSecondTaskError() =>
            Ok<string, int>("banana")
                .Async()
                .BindError(_ => Error<string, int>(42).ToTask())
                .ShouldBeOk("banana");
    }
}
