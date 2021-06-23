namespace DeFuncto
{
    public static class Prelude
    {
        public static ResultOk<TOk> Ok<TOk>(TOk ok) => new(ok);
        public static ResultError<TError> Error<TError>(TError error) => new(error);
    }
}
