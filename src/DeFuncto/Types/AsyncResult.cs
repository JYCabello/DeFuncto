using System;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct AsyncResult<TOk, TError>
    {
        private readonly Task<Result<TOk, TError>> resultTask;

        private AsyncResult(Result<TOk, TError> result) : this(Task.FromResult(result)) { }

        private AsyncResult(Task<Result<TOk, TError>> resultTask) =>
            this.resultTask = resultTask;

        public static implicit operator AsyncResult<TOk, TError>(Result<TOk, TError> result) =>
            new(result);

        public static implicit operator AsyncResult<TOk, TError>(TOk ok) =>
            Ok<TOk, TError>(ok);

        public static implicit operator AsyncResult<TOk, TError>(TError error) =>
            Error<TOk, TError>(error);

        public static implicit operator AsyncResult<TOk, TError>(Task<Result<TOk, TError>> result) =>
            new(result);

        public static implicit operator AsyncResult<TOk, TError>(Task<TOk> ok) =>
            ok.Map(Ok<TOk, TError>);

        public static implicit operator AsyncResult<TOk, TError>(Task<TError> error) =>
            error.Map(Error<TOk, TError>);

        public Task<Result<TOk, TError>> Result() => resultTask;

        public AsyncResult<TOk2, TError> Map<TOk2>(Func<TOk, TOk2> f) =>
            resultTask.Map(r => r.Map(f));

        public AsyncResult<TOk2, TError> Map<TOk2>(Func<TOk, Task<TOk2>> f) =>
            Match(
                ok => f(ok).Map(Ok<TOk2, TError>),
                error => error.Apply(Error<TOk2, TError>).Apply(Task.FromResult)
            );

        public AsyncResult<TOk, TError2> MapError<TError2>(Func<TError, TError2> f) =>
            Match(Ok<TOk, TError2>, e => f(e));

        public AsyncResult<TOk, TError2> MapError<TError2>(Func<TError, Task<TError2>> f) =>
            Match(
                ok => ok.Apply(Ok<TOk, TError2>).Apply(Task.FromResult),
                error => f(error).Map(Error<TOk, TError2>)
            );

        public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, Result<TOk2, TError>> f) =>
            Match(f, Error<TOk2, TError>);

        public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, Task<Result<TOk2, TError>>> f) =>
            Match(f, error => error.Apply(Error<TOk2, TError>).Apply(Task.FromResult));

        public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, AsyncResult<TOk2, TError>> f) =>
            Bind(ok => f(ok).Result());

        public Task<TOut> Match<TOut>(Func<TOk, TOut> fOk, Func<TError, TOut> fError) =>
            resultTask.Map(r => r.Match(fOk, fError));

        public Task<Result<TOk, TError>> Iter(Func<TOk, Task<Unit>> fOk, Func<TError, Task<Unit>> fError) =>
            Iter(async ok => { await fOk(ok); }, async error => { await fError(error); });

        public Task<Result<TOk, TError>> Iter(Func<TOk, Task> fOk, Func<TError, Task> fError) =>
            Match(
                async ok =>
                {
                    await fOk(ok);
                    return Ok<TOk, TError>(ok);
                },
                async error =>
                {
                    await fError(error);
                    return Error<TOk, TError>(error);
                }
            );

        public Task<Result<TOk, TError>> Iter(Func<TOk, Task<Unit>> fOk) =>
            Iter(fOk, _ => unit.Apply(Task.FromResult));

        public Task<Result<TOk, TError>> Iter(Func<TOk, Task> fOk) =>
            Iter(fOk, _ => unit.Apply(Task.FromResult));

        public Task<Result<TOk, TError>> Iter(Func<TOk, Unit> fOk) =>
            Iter(ok => fOk(ok).Apply(Task.FromResult));

        public Task<Result<TOk, TError>> Iter(Action<TOk> fOk) =>
            Iter(ok =>
            {
                fOk(ok);
                return unit;
            });

        public Task<Result<TOk, TError>> Iter(Func<TError, Task<Unit>> fError) =>
            Iter(_ => unit.Apply(Task.FromResult), fError);

        public Task<Result<TOk, TError>> Iter(Func<TError, Task> fError) =>
            Iter(_ => unit.Apply(Task.FromResult), fError);

        public Task<Result<TOk, TError>> Iter(Func<TError, Unit> fError) =>
            Iter(ok => fError(ok).Apply(Task.FromResult));

        public Task<Result<TOk, TError>> Iter(Action<TError> fError) =>
            Iter(ok =>
            {
                fError(ok);
                return unit;
            });

        public async Task<TOut> Match<TOut>(Func<TOk, Task<TOut>> fOk, Func<TError, Task<TOut>> fError) =>
            await resultTask.Map(r => r.Match(fOk, fError));

        public AsyncResult<TOk2, TError> Select<TOk2>(Func<TOk, TOk2> projection) => Map(projection);

        public AsyncResult<TOkFinal, TError> SelectMany<TOkBind, TOkFinal>(
            Func<TOk, AsyncResult<TOkBind, TError>> binder,
            Func<TOk, TOkBind, TOkFinal> projection
        ) =>
            Bind(ok => binder(ok).Map(okbind => (ok, okbind)))
                .Match(okTpl => Ok<TOkFinal, TError>(projection(okTpl.ok, okTpl.okbind)), Error<TOkFinal, TError>);

        public Task<bool> IsOk => resultTask.Map(r => r.IsOk);
        public Task<bool> IsError => resultTask.Map(r => r.IsError);
    }

    public static class AsyncResultExtensions
    {
        public static Task<T> Collapse<T>(this AsyncResult<T, T> self) => self.Match(Id, Id);
    }
}
