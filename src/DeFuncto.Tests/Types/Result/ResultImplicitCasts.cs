using Xunit;

namespace DeFuncto.Tests.Types.Result
{
    public class ResultImplicitCasts
    {
        [Fact(DisplayName = "Implicit cast from ResultOk")]
        public void CastsResultOk()
        {
            Result<string, int> ok = new ResultOk<string>("banana");
            Assert.True(ok.IsOk);
            Assert.Equal("banana", ok.OkValue);
        }

        [Fact(DisplayName = "Implicit cast from Ok type")]
        public void CastsOk()
        {
            Result<string, int> ok = "banana";
            Assert.True(ok.IsOk);
            Assert.Equal("banana", ok.OkValue);
        }

        [Fact(DisplayName = "Implicit cast from ResultError")]
        public void CastsResultError()
        {
            Result<int, string> error = new ResultError<string>("banana");
            Assert.True(error.IsError);
            Assert.Equal("banana", error.ErrorValue);
        }

        [Fact(DisplayName = "Implicit cast from error type")]
        public void CastsError()
        {
            Result<int, string> error = "banana";
            Assert.True(error.IsError);
            Assert.Equal("banana", error.ErrorValue);
        }
    }
}
