namespace DeFuncto
{
    public static class Prelude
    {
        public static ResultOk<TOk> Ok<TOk>(TOk ok) => new(ok);
        public static Result<TOk, TError> Ok<TOk, TError>(TOk ok) => Result<TOk, TError>.Ok(ok);
        public static ResultError<TError> Error<TError>(TError error) => new(error);
        public static Result<TOk, TError> Error<TOk, TError>(TError error) => Result<TOk, TError>.Error(error);
        public static T Id<T>(T t) => t;
    }
}
