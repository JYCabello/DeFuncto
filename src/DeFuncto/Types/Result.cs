using System;
using System.Threading.Tasks;
using DeFuncto.Extensions;
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
            Match(ok => projection(ok).Apply(Ok<TOk2, TError>), Error<TOk2, TError>);

        public Result<TOk2, TError> Select<TOk2>(Func<TOk, TOk2> projection) => Map(projection);

        public Result<TOk, TError2> MapError<TError2>(Func<TError, TError2> projection) =>
            Match(Ok<TOk, TError2>, error => projection(error).Apply(Error<TOk, TError2>));

        public Result<TOk2, TError> Bind<TOk2>(Func<TOk, Result<TOk2, TError>> binder) =>
            Match(binder, Error<TOk2, TError>);

        public Result<TOk, TError2> BindError<TError2>(Func<TError, Result<TOk, TError2>> binder) =>
            Match(Ok<TOk, TError2>, binder);

        public Result<TOkFinal, TError> SelectMany<TOkBind, TOkFinal>(
            Func<TOk, Result<TOkBind, TError>> binder,
            Func<TOk, TOkBind, TOkFinal> projection
        ) =>
            Bind(ok => binder(ok).Map(okbind => (ok, okbind)))
                .Match(okTpl => Ok<TOkFinal, TError>(projection(okTpl.ok, okTpl.okbind)), Error<TOkFinal, TError>);

        public TOut Match<TOut>(Func<TOk, TOut> okProjection, Func<TError, TOut> errorProjection) =>
            IsOk ? okProjection(OkValue!) : errorProjection(ErrorValue!);

        public Result<TOk, TError> Iter(Action<TOk> iterator)
        {
            if (IsOk)
                iterator(OkValue!);
            return this;
        }

        public Result<TOk, TError> Iter(Func<TOk, Unit> iterator) =>
            Iter(ok => { iterator(ok); });

        public Result<TOk, TError> Iter(Action<TError> iterator)
        {
            if (IsError)
                iterator(ErrorValue!);
            return this;
        }

        public Result<TOk, TError> Iter(Func<TError, Unit> iterator) =>
            Iter(error => { iterator(error); });

        public Result<TOk, TError> Iter(Func<TOk, Unit> iteratorOk, Func<TError, Unit> iteratorError) =>
            this.Apply(self => self.Match(iteratorOk, iteratorError).Apply(_ => self));

        public Result<TOk, TError> Iter(Action<TOk> iteratorOk, Action<TError> iteratorError)
        {
            Iter(iteratorOk);
            return Iter(iteratorError);
        }

        internal AsyncResult<TOk, TError> ToAsync() => Task.FromResult(this);

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
        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Task<Result<TOk, TError>> self) => self;
        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<TOk, TError> self) => self;

        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<TOk, Task<TError>> self) =>
            self.Match(ok => ok.Apply(Ok<TOk, TError>).ToAsync(), errTsk => errTsk.Map(Error<TOk, TError>));

        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<Task<TOk>, TError> self) =>
            self.Match(okTsk => okTsk.Map(Ok<TOk, TError>).Async(), error => error.Apply(Error<TOk, TError>));

        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<Task<TOk>, Task<TError>> self) =>
            self.Match(okTsk => okTsk.Map(Ok<TOk, TError>), errTsk => errTsk.Map(Error<TOk, TError>));

        public static AsyncResult<TOk, TError> Async<TOk, TError>(this Task<Result<Task<TOk>, Task<TError>>> self) =>
            self.Map(r => r.Async().Result());
    }
}
