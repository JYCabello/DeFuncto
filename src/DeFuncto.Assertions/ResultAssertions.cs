using System;
using static DeFuncto.Prelude;

namespace DeFuncto.Assertions
{
    public static class ResultAssertions
    {
        public static Result<TOk, TError> ShouldBeOk<TOk, TError>(this Result<TOk, TError> self)
        {
            if (self.IsError)
                throw new AssertionFailedException("Result should be Ok, but it was Error");
            return self;
        }

        public static Result<TOk, TError> ShouldBeOk<TOk, TError>(
            this Result<TOk, TError> self,
            Func<TOk, Unit> assertion
        ) =>
            self.ShouldBeOk().Iter(assertion);

        public static Result<TOk, TError> ShouldBeOk<TOk, TError>(
            this Result<TOk, TError> self,
            TOk expected
        ) =>
            self.ShouldBeOk(val =>
            {
                if (!val.Equals(expected))
                    throw new AssertionFailedException($"Ok value should be {expected} but it was {val}");
                return unit;
            });
    }
}
