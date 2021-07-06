using System;
using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class BindError
    {
        [Fact(DisplayName = "Binds on both Error with a sync bind")]
        public void BothError() =>
            Error<string, string>("ba")
                .Async()
                .BindError(val => Error<string, string>($"{val}nana"))
                .ShouldBeError("banana");

        [Fact(DisplayName = "Gives second Ok on first error with a sync bind")]
        public void FirstErrorSecondNot() =>
            Error<string, string>("pear")
                .Async()
                .BindError(_ => Ok<string, string>("banana").Async())
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Gives first error on second Ok with task")]
        public void FirstErrorSecondOk() =>
            Error<int, string>("banana")
                .Async()
                .BindError(_ => Ok<int, string>(42).ToTask())
                .ShouldBeError("banana");

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
