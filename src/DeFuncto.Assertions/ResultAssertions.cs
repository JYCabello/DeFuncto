using System;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto.Assertions
{
    public static class ResultAssertions
    {
        public static Result<TOk, TError> ShouldBeOk<TOk, TError>(this Result<TOk, TError> self)
        {
            if (self.IsError)
                throw new AssertionFailed("Result should be Ok, but it was Error");
            return self;
        }

        public static Task<Result<TOk, TError>> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self) =>
            self.Result().Map(ShouldBeOk);

        public static Result<TOk, TError> ShouldBeOk<TOk, TError>(
            this Result<TOk, TError> self,
            Func<TOk, Unit> assertion
        ) =>
            self.ShouldBeOk().Iter(assertion);

        public static Task<Result<TOk, TError>> ShouldBeOk<TOk, TError>(
            this AsyncResult<TOk, TError> self,
            Func<TOk, Unit> assertion
        ) =>
            self.Result().Map(result => result.ShouldBeOk(assertion));

        public static Result<TOk, TError> ShouldBeOk<TOk, TError>(
            this Result<TOk, TError> self,
            TOk expected
        ) =>
            self.ShouldBeOk(val =>
            {
                if (!val.Equals(expected))
                    throw new AssertionFailed($"Ok value should be {expected} but it was {val}");
                return unit;
            });

        public static Task<Result<TOk, TError>> ShouldBeOk<TOk, TError>(
            this AsyncResult<TOk, TError> self,
            TOk ok
        ) =>
            self.Result().Map(result => result.ShouldBeOk(ok));

        public static Result<TOk, TError> ShouldBeError<TOk, TError>(this Result<TOk, TError> self)
        {
            if (self.IsOk)
                throw new AssertionFailed("Result should be Error, but it was Ok");
            return self;
        }

        public static Result<TOk, TError> ShouldBeError<TOk, TError>(
            this Result<TOk, TError> self,
            Func<TError, Unit> assertion
        ) =>
            self.ShouldBeError().Iter(assertion);

        public static Result<TOk, TError> ShouldBeError<TOk, TError>(
            this Result<TOk, TError> self,
            TError expected
        ) =>
            self.ShouldBeError(val =>
            {
                if (!val.Equals(expected))
                    throw new AssertionFailed($"Error value should be {expected} but it was {val}");
                return unit;
            });
    }
}
