using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct AsyncResult<TOk, TError>
    {
        private readonly Task<Result<TOk, TError>> resultTask;

        public AsyncResult(Result<TOk, TError> result) : this(Task.FromResult(result)) { }

        public AsyncResult(Task<Result<TOk, TError>> resultTask) =>
            this.resultTask = resultTask;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator AsyncResult<TOk, TError>(Result<TOk, TError> result) =>
            new(result);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator AsyncResult<TOk, TError>(TOk ok) =>
            Ok<TOk, TError>(ok);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator AsyncResult<TOk, TError>(TError error) =>
            Error<TOk, TError>(error);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator AsyncResult<TOk, TError>(Task<Result<TOk, TError>> result) =>
            new(result);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator AsyncResult<TOk, TError>(Task<TOk> ok) =>
            ok.Map(Ok<TOk, TError>);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator AsyncResult<TOk, TError>(Task<TError> error) =>
            error.Map(Error<TOk, TError>);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Result<TOk, TError>> Result() => resultTask;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk2, TError> Map<TOk2>(Func<TOk, TOk2> f) =>
            resultTask.Map(r => r.Map(f));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk2, TError> Map<TOk2>(Func<TOk, Task<TOk2>> f) =>
            Match(
                ok => f(ok).Map(Ok<TOk2, TError>),
                error => error.Apply(Error<TOk2, TError>).Apply(Task.FromResult)
            );

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk, TError2> MapError<TError2>(Func<TError, TError2> f) =>
            Match(Ok<TOk, TError2>, e => f(e));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk, TError2> MapError<TError2>(Func<TError, Task<TError2>> f) =>
            Match(
                ok => ok.Apply(Ok<TOk, TError2>).Apply(Task.FromResult),
                error => f(error).Map(Error<TOk, TError2>)
            );

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, Result<TOk2, TError>> f) =>
            Match(f, Error<TOk2, TError>);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, Task<Result<TOk2, TError>>> f) =>
            Match(f, error => error.Apply(Error<TOk2, TError>).Apply(Task.FromResult));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, AsyncResult<TOk2, TError>> f) =>
            Bind(ok => f(ok).Result());

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk, TError2> BindError<TError2>(Func<TError, Result<TOk, TError2>> f) =>
            Match(Ok<TOk, TError2>, f);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk, TError2> BindError<TError2>(Func<TError, Task<Result<TOk, TError2>>> f) =>
            Match(ok => Ok<TOk, TError2>(ok).Apply(Task.FromResult), f);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk, TError2> BindError<TError2>(Func<TError, AsyncResult<TOk, TError2>> f) =>
            BindError(error => f(error).Result());

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TOut> Match<TOut>(Func<TOk, TOut> fOk, Func<TError, TOut> fError) =>
            resultTask.Map(r => r.Match(fOk, fError));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Result<TOk, TError>> Iter(Func<TOk, Task<Unit>> fOk, Func<TError, Task<Unit>> fError) =>
            Iter(async ok => { await fOk(ok); }, async error => { await fError(error); });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Result<TOk, TError>> Iter(Func<TOk, Task<Unit>> fOk) =>
            Iter(fOk, _ => unit.Apply(Task.FromResult));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Result<TOk, TError>> Iter(Func<TOk, Task> fOk) =>
            Iter(fOk, _ => unit.Apply(Task.FromResult));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Result<TOk, TError>> Iter(Func<TOk, Unit> fOk) =>
            Iter(ok => fOk(ok).Apply(Task.FromResult));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Result<TOk, TError>> Iter(Action<TOk> fOk) =>
            Iter(ok =>
            {
                fOk(ok);
                return unit;
            });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Result<TOk, TError>> Iter(Func<TError, Task<Unit>> fError) =>
            Iter(_ => unit.Apply(Task.FromResult), fError);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Result<TOk, TError>> Iter(Func<TError, Task> fError) =>
            Iter(_ => unit.Apply(Task.FromResult), fError);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Result<TOk, TError>> Iter(Func<TError, Unit> fError) =>
            Iter(ok => fError(ok).Apply(Task.FromResult));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Result<TOk, TError>> Iter(Action<TError> fError) =>
            Iter(ok =>
            {
                fError(ok);
                return unit;
            });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TOut> Match<TOut>(Func<TOk, Task<TOut>> fOk, Func<TError, Task<TOut>> fError) =>
            await resultTask.Map(r => r.Match(fOk, fError));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<TOk2, TError> Select<TOk2>(Func<TOk, TOk2> projection) => Map(projection);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> Collapse<T>(this AsyncResult<T, T> self) => self.Match(Id, Id);
    }
}
