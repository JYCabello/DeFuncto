using System;

namespace DeFuncto
{
    public readonly struct Result<TOk, TError>
    {
        internal readonly TError? ErrorValue;
        internal readonly TOk? OkValue;
        public readonly bool IsOk;
        public bool IsError => !IsOk;

        private Result(TError error)
        {
            ErrorValue = error;
            OkValue = default;
            IsOk = false;
        }

        private Result(TOk ok)
        {
            ErrorValue = default;
            OkValue = ok;
            IsOk = true;
        }

        public static Result<TOk, TError> Ok(TOk right) => new(right);
        public static Result<TOk, TError> Error(TError left) => new(left);

        public Result<TOk2, TError> Map<TOk2>(Func<TOk, TOk2> projection) =>
            IsOk ? new Result<TOk2, TError>(projection(OkValue!)) : new Result<TOk2, TError>(ErrorValue!);

        public Result<TOk2, TError> Select<TOk2>(Func<TOk, TOk2> projection) => Map(projection);

        public Result<TOk2, TError> Bind<TOk2>(Func<TOk, Result<TOk2, TError>> binder) =>
            IsOk ? binder(OkValue!) : new Result<TOk2, TError>(ErrorValue!);

        public Result<TOkFinal, TError> SelectMany<TOkBind, TOkFinal>(
            Func<TOk, Result<TOkBind, TError>> binder,
            Func<TOk, TOkBind, TOkFinal> projection
        )
        {
            if (IsError)
                return new Result<TOkFinal, TError>(ErrorValue!);
            var bound = Bind(binder);
            return bound.IsOk
                ? new Result<TOkFinal, TError>(projection(OkValue!, bound.OkValue!))
                : new Result<TOkFinal, TError>(bound.ErrorValue!);
        }

        public static implicit operator Result<TOk, TError>(ResultOk<TOk> resultOk) => Ok(resultOk.OkValue);
        public static implicit operator Result<TOk, TError>(TOk ok) => Ok(ok);
        public static implicit operator Result<TOk, TError>(ResultError<TError> resultError) => Error(resultError.ErrorValue);
        public static implicit operator Result<TOk, TError>(TError error) => Error(error);
    }

    public readonly struct ResultOk<TOk>
    {
        internal readonly TOk OkValue;

        public ResultOk(TOk okValue) =>
            OkValue = okValue;
    }
    public readonly struct ResultError<TError>
    {
        internal readonly TError ErrorValue;

        public ResultError(TError errorValue) =>
            ErrorValue = errorValue;
    }
}
