using System;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.Result
{
    public class Bind
    {
        [Fact(DisplayName = "Binds on both Ok")]
        public void BothOk() =>
            Ok<string, int>("ba")
                .Bind(val => Result<string, int>.Ok($"{val}nana"))
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Gives second error on first Ok")]
        public void FirstOkSecondNot() =>
            Ok<string, string>("banana")
                .Bind(val =>
                    int.TryParse(val, out var number)
                        ? Ok<int, string>(number)
                        : Error<int, string>($"{val} was not a number")
                )
                .ShouldBeError("banana was not a number");

        [Fact(DisplayName = "Gives first error on second Ok")]
        public void FirstErrorSecondOk() =>
            Error<int, string>("banana")
                .Bind(_ => Result<int, string>.Ok(42))
                .ShouldBeError("banana");

        [Fact(DisplayName = "Gives first error on second error")]
        public void FirstErrorSecondAlso() =>
            Error<int, string>("banana")
                .Bind(_ => Result<int, string>.Error("pear"))
                .ShouldBeError("banana");

        [Fact(DisplayName = "Does not run the binder on first error")]
        public void FirstErrorNoBinderRun() =>
            Error<int, string>("banana")
                .Bind<int>(_ => throw new Exception("Should not run"))
                .ShouldBeError("banana");
    }
}
