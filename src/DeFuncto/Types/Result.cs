using System;
using static DeFuncto.Prelude;

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

        public Result<TOk, TError2> MapError<TError2>(Func<TError, TError2> projection) =>
            IsError ? new Result<TOk, TError2>(projection(ErrorValue!)) : new Result<TOk, TError2>(OkValue!);

        public Result<TOk2, TError> Bind<TOk2>(Func<TOk, Result<TOk2, TError>> binder) =>
            IsOk ? binder(OkValue!) : new Result<TOk2, TError>(ErrorValue!);

        public Result<TOk, TError2> BindError<TError2>(Func<TError, Result<TOk, TError2>> binder) =>
            IsError ? binder(ErrorValue!) : new Result<TOk, TError2>(OkValue!);

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

        public TOut Match<TOut>(Func<TOk, TOut> okProjection, Func<TError, TOut> errorProjection) =>
            IsOk ? okProjection(OkValue!) : errorProjection(ErrorValue!);

        public Unit Iter(Action<TOk> iterator)
        {
            if (IsOk)
                iterator(OkValue!);
            return unit;
        }

        public Unit Iter(Func<TOk, Unit> iterator) =>
            Iter(ok => { iterator(ok); });

        public Unit Iter(Action<TError> iterator)
        {
            if (IsError)
                iterator(ErrorValue!);
            return unit;
        }

        public Unit Iter(Func<TError, Unit> iterator) =>
            Iter(error => { iterator(error); });

        public Unit Iter(Func<TOk, Unit> iteratorOk, Func<TError, Unit> iteratorError) =>
            Match(iteratorOk, iteratorError);

        public Unit Iter(Action<TOk> iteratorOk, Action<TError> iteratorError)
        {
            Iter(iteratorOk);
            return Iter(iteratorError);
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

        public Result<TOk, TError> ToResult<TError>() => this;
    }

    public readonly struct ResultError<TError>
    {
        internal readonly TError ErrorValue;

        public ResultError(TError errorValue) =>
            ErrorValue = errorValue;

        public Result<TOk, TError> ToResult<TOk>() => this;
    }

    public static class ResultExtensions
    {
        public static TOut Collapse<TOut>(this Result<TOut, TOut> result) => result.Match(Id, Id);
    }
}
