using DeFuncto.Assertions;
using Xunit;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class ResultImplicitCasts
    {
        [Fact(DisplayName = "Implicit cast from ResultOk")]
        public void CastsResultOk()
        {
            Result<string, int> ok = new ResultOk<string>("banana");
            ok.ShouldBeOk("banana");
        }

        [Fact(DisplayName = "Implicit cast from Ok type")]
        public void CastsOk()
        {
            Result<string, int> ok = "banana";
            ok.ShouldBeOk("banana");
        }

        [Fact(DisplayName = "Implicit cast from ResultError")]
        public void CastsResultError()
        {
            Result<int, string> error = new ResultError<string>("banana");
            error.ShouldBeError("banana");
        }

        [Fact(DisplayName = "Implicit cast from error type")]
        public void CastsError()
        {
            Result<int, string> error = "banana";
            error.ShouldBeError("banana");
        }
    }
}
