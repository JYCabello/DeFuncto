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

        public static Result<TOk, TError> ShouldBeOk<TOk, TError>(this Result<TOk, TError> self, Func<TOk, Unit> assertion) =>
            self.ShouldBeOk().Iter(assertion);

        public static Task<Result<TOk, TError>> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self, Func<TOk, Unit> assertion) =>
            self.ShouldBeOk().Async().Iter(assertion);

        public static Result<TOk, TError> ShouldBeOk<TOk, TError>(this Result<TOk, TError> self, TOk expected) =>
            self.ShouldBeOk(val => val.AssertEquals(expected));

        public static Unit AssertEquals<T>(this T self, T other)
        {
            if (!self.Equals(other))
                throw new AssertionFailed($"Ok value should be {other} but it was {self}");
            return unit;
        }

        public static Task<Result<TOk, TError>> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self, TOk expected) =>
            self.Result().Map(result => result.ShouldBeOk(ok => ok.AssertEquals(expected)));

        public static Result<TOk, TError> ShouldBeError<TOk, TError>(this Result<TOk, TError> self)
        {
            if (self.IsOk)
                throw new AssertionFailed("Result should be Error, but it was Ok");
            return self;
        }

        public static Task<Result<TOk, TError>> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self) =>
            self.Result().Map(ShouldBeError);

        public static Result<TOk, TError> ShouldBeError<TOk, TError>(this Result<TOk, TError> self, Func<TError, Unit> assertion) =>
            self.ShouldBeError().Iter(assertion);

        public static Task<Result<TOk, TError>> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self, Func<TError, Unit> assertion) =>
            self.Result().Map(result => result.ShouldBeError(assertion));

        public static Result<TOk, TError> ShouldBeError<TOk, TError>(this Result<TOk, TError> self, TError expected) =>
            self.ShouldBeError(val => val.AssertEquals(expected));

        public static Task<Result<TOk, TError>> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self, TError expected) =>
            self.Result().Map(result => result.ShouldBeError(expected));

    }
}
