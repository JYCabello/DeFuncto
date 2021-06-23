using System;
using Xunit;

namespace DeFuncto.Tests.Types.Result
{
    public class BindError
    {
        [Fact(DisplayName = "Binds on both Error")]
        public void BothError()
        {
            var ok = Result<string, string>.Error("ba");
            var result = ok.BindError(val => Result<string, string>.Error($"{val}nana"));
            Assert.True(result.IsError);
            Assert.Equal("banana", result.ErrorValue);
        }

        [Fact(DisplayName = "Gives second Ok on first error")]
        public void FirstErrorSecondNot()
        {
            var ok = Result<string, string>.Error("pear");
            var result = ok.BindError(val => Result<string, string>.Ok("banana"));
            Assert.True(result.IsOk);
            Assert.Equal("banana", result.OkValue);
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
