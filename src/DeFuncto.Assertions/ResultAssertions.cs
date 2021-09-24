using System;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto.Assertions
{
    public static class ResultAssertions
    {
        public static Unit ShouldBeOk<TOk, TError>(this Result<TOk, TError> self)
        {
            if (self.IsError)
                throw new AssertionFailed("Result should be Ok, but it was Error");
            return unit;
        }

        public static Task<Unit> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self) =>
            self.Result().Map(ShouldBeOk);

        public static Unit ShouldBeOk<TOk, TError>(this Result<TOk, TError> self, Func<TOk, Unit> assertion)
        {
            self.ShouldBeOk();
            return self.Iter(assertion);
        }

        public static async Task<Unit> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self, Func<TOk, Unit> assertion)
        {
            await self.ShouldBeOk();
            return await self.Iter(assertion);
        }

        public static Unit ShouldBeOk<TOk, TError>(this Result<TOk, TError> self, TOk expected) =>
            self.ShouldBeOk(val => val.AssertEquals(expected));

        public static Unit AssertEquals<T>(this T self, T other)
        {
            if (!self.Equals(other))
                throw new AssertionFailed($"Expected {other} but it was {self}");
            return unit;
        }

        public static Task<Unit> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self, TOk expected) =>
            self.Result().Map(result => result.ShouldBeOk(ok => ok.AssertEquals(expected)));

        public static Result<TOk, TError> ShouldBeError<TOk, TError>(this Result<TOk, TError> self)
        {
            if (self.IsOk)
                throw new AssertionFailed("Result should be Error, but it was Ok");
            return self;
        }

        public static Task<Result<TOk, TError>> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self) =>
            self.Result().Map(ShouldBeError);

        public static Unit ShouldBeError<TOk, TError>(this Result<TOk, TError> self, Func<TError, Unit> assertion) =>
            self.ShouldBeError().Iter(assertion);

        public static Task<Unit> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self, Func<TError, Unit> assertion) =>
            self.Result().Map(result => result.ShouldBeError(assertion));

        public static Unit ShouldBeError<TOk, TError>(this Result<TOk, TError> self, TError expected) =>
            self.ShouldBeError(val => val.AssertEquals(expected));

        public static Task<Unit> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self, TError expected) =>
            self.Result().Map(result => result.ShouldBeError(expected));

    }
}
