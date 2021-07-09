using System;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class BindError
    {
        [Fact(DisplayName = "Binds on both Error")]
        public void BothError() =>
            Error<string, string>("ba")
                .BindError(val => Error<string, string>($"{val}nana"))
                .ShouldBeError("banana");

        [Fact(DisplayName = "Gives second Ok on first error")]
        public void FirstErrorSecondNot() =>
            Error<string, string>("pear")
                .BindError(_ => Ok<string, string>("banana"))
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Gives first error on second Ok")]
        public void FirstErrorSecondOk() =>
            Error<int, string>("banana")
                .Bind(_ => Ok<int, string>(42))
                .ShouldBeError("banana");

        [Fact(DisplayName = "Gives first error on second error")]
        public void FirstErrorSecondAlso() =>
            Error<int, string>("banana")
                .Bind(_ => Error<int, string>("pear"))
                .ShouldBeError("banana");

        [Fact(DisplayName = "Does not run the binder on first error")]
        public void FirstErrorNoBinderRun() =>
            Error<int, string>("banana")
                .Bind<int>(_ => throw new Exception("Should not run"))
                .ShouldBeError("banana");
    }
}
