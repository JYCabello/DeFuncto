using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class ResultImplicitCasts
    {
        [Property(DisplayName = "Implicit cast from ResultOk")]
        public void CastsResultOk(NonNull<string> a) =>
            ((Result<string, int>) new ResultOk<string>(a.Get)).ShouldBeOk(a.Get);

        [Property(DisplayName = "Implicit cast from Ok type")]
        public void CastsOk(NonNull<string> a) =>
            ((Result<string, int>) a.Get).ShouldBeOk(a.Get);

        [Property(DisplayName = "Implicit cast from ResultError")]
        public void CastsResultError(NonNull<string> a) =>
            ((Result<int, string>) new ResultError<string>(a.Get)).ShouldBeError(a.Get);

        [Property(DisplayName = "Implicit cast from error type")]
        public void CastsError(NonNull<string> a) =>
            ((Result<int, string>) a.Get).ShouldBeError(a.Get);
    }
}
