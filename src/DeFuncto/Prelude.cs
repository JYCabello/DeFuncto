using System;

namespace DeFuncto
{
    public static class Prelude
    {
        public static ResultOk<TOk> Ok<TOk>(TOk ok) => new(ok);
        public static Result<TOk, TError> Ok<TOk, TError>(TOk ok) => Ok(ok);
        public static ResultError<TError> Error<TError>(TError error) => new(error);
        public static Result<TOk, TError> Error<TOk, TError>(TError error) => Error(error);
        public static T Id<T>(T t) => t;
        // ReSharper disable once InconsistentNaming
        public static Unit unit => Unit.Default;
        public static OptionNone None => new();
        public static Option<T> Some<T>(T value) => Option<T>.Some(value);

        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> f1, Func<T2, T3> f2) =>
            t1 => f2(f1(t1));
    }
}
