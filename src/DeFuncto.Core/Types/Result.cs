using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct Result<TOk, TError>
    {
        private readonly TError? errorValue;
        private readonly TOk? okValue;
        public readonly bool IsOk;
        public bool IsError => !IsOk;

        public Result(TError error)
        {
            errorValue = error;
            okValue = default;
            IsOk = false;
        }

        public Result(TOk ok)
        {
            errorValue = default;
            okValue = ok;
            IsOk = true;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TOk, TError> Ok(TOk right) => new(right);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TOk, TError> Error(TError left) => new(left);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk2, TError> Map<TOk2>(Func<TOk, TOk2> projection) =>
            Match(ok => projection(ok).Apply(Ok<TOk2, TError>), Error<TOk2, TError>);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk2, TError> Select<TOk2>(Func<TOk, TOk2> projection) => Map(projection);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk, TError2> MapError<TError2>(Func<TError, TError2> projection) =>
            Match(Ok<TOk, TError2>, error => projection(error).Apply(Error<TOk, TError2>));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk2, TError> Bind<TOk2>(Func<TOk, Result<TOk2, TError>> binder) =>
            Match(binder, Error<TOk2, TError>);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk, TError2> BindError<TError2>(Func<TError, Result<TOk, TError2>> binder) =>
            Match(Ok<TOk, TError2>, binder);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOkFinal, TError> SelectMany<TOkBind, TOkFinal>(
            Func<TOk, Result<TOkBind, TError>> binder,
            Func<TOk, TOkBind, TOkFinal> projection
        ) =>
            Bind(ok => binder(ok).Map(okbind => projection(ok, okbind)));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TOut Match<TOut>(Func<TOk, TOut> okProjection, Func<TError, TOut> errorProjection) =>
            IsOk ? okProjection(okValue!) : errorProjection(errorValue!);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk, TError> Iter(Action<TOk> iterator)
        {
            if (IsOk)
                iterator(okValue!);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk, TError> Iter(Func<TOk, Unit> iterator) =>
            Iter(ok => { iterator(ok); });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk, TError> Iter(Action<TError> iterator)
        {
            if (IsError)
                iterator(errorValue!);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk, TError> Iter(Func<TError, Unit> iterator) =>
            Iter(error => { iterator(error); });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk, TError> Iter(Func<TOk, Unit> iteratorOk, Func<TError, Unit> iteratorError) =>
            this.Apply(self => self.Match(iteratorOk, iteratorError).Apply(_ => self));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk, TError> Iter(Action<TOk> iteratorOk, Action<TError> iteratorError)
        {
            Iter(iteratorOk);
            return Iter(iteratorError);
        }

        public Option<TOk> Option => Match(Some, _ => None);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TOk, TError>(ResultOk<TOk> resultOk) => Ok(resultOk.OkValue);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TOk, TError>(TOk ok) => Ok(ok);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TOk, TError>(ResultError<TError> resultError) => Error(resultError.ErrorValue);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TOk, TError>(TError error) => Error(error);
    }

    public readonly struct ResultOk<TOk>
    {
        internal readonly TOk OkValue;

        public ResultOk(TOk okValue) =>
            OkValue = okValue;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk, TError> ToResult<TError>() => this;
    }

    public readonly struct ResultError<TError>
    {
        internal readonly TError ErrorValue;

        public ResultError(TError errorValue) =>
            ErrorValue = errorValue;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TOk, TError> ToResult<TOk>() => this;
    }

    public static class ResultExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOut Collapse<TOut>(this Result<TOut, TOut> result) => result.Match(Id, Id);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Task<Result<TOk, TError>> self) => self;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<TOk, TError> self) => self;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<TOk, Task<TError>> self) =>
            self.Match(ok => ok.Apply(Ok<TOk, TError>).Async(), errTsk => errTsk.Map(Error<TOk, TError>));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<Task<TOk>, TError> self) =>
            self.Match(okTsk => okTsk.Map(Ok<TOk, TError>).Async(), error => error.Apply(Error<TOk, TError>));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<Task<TOk>, Task<TError>> self) =>
            self.Match(okTsk => okTsk.Map(Ok<TOk, TError>), errTsk => errTsk.Map(Error<TOk, TError>));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Task<Result<Task<TOk>, Task<TError>>> self) =>
            self.Map(r => r.Async().Result());
    }
}
