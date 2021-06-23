using System;
using DeFuncto.Assertions;
using Xunit;

namespace DeFuncto.Tests.Types.Result
{
    public class Bind
    {
        [Fact(DisplayName = "Binds on both Ok")]
        public void BothOk()
        {
            var ok = Result<string, int>.Ok("ba");
            var result = ok.Bind(val => Result<string, int>.Ok($"{val}nana"));
            result.ShouldBeOk("banana");
        }

        [Fact(DisplayName = "Gives second error on first Ok")]
        public void FirstOkSecondNot()
        {
            var ok = Result<string, string>.Ok("banana");
            var result = ok.Bind(val =>
                int.TryParse(val, out var number) ? Result<int, string>.Ok(number) : Result<int, string>.Error($"{val} was not a number")
            );
            Assert.True(result.IsError);
            Assert.NotNull(result.ErrorValue);
            Assert.Equal("banana was not a number", result.ErrorValue);
        }

        [Fact(DisplayName = "Gives first error on second Ok")]
        public void FirstErrorSecondOk()
        {
            var ok = Result<int, string>.Error("banana");
            var result = ok.Bind(_ => Result<int, string>.Ok(42));
            Assert.True(result.IsError);
            Assert.NotNull(result.ErrorValue);
            Assert.Equal("banana", result.ErrorValue);
        }

        [Fact(DisplayName = "Gives first error on second error")]
        public void FirstErrorSecondAlso()
        {
            var ok = Result<int, string>.Error("banana");
            var result = ok.Bind(_ => Result<int, string>.Error("pear"));
            Assert.True(result.IsError);
            Assert.NotNull(result.ErrorValue);
            Assert.Equal("banana", result.ErrorValue);
        }

        [Fact(DisplayName = "Does not run the binder on first error")]
        public void FirstErrorNoBinderRun()
        {
            var ok = Result<int, string>.Error("banana");
            var result = ok.Bind<int>(_ => throw new Exception("Should not run"));
            Assert.True(result.IsError);
            Assert.NotNull(result.ErrorValue);
            Assert.Equal("banana", result.ErrorValue);
        }
    }
}
