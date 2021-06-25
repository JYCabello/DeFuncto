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

        public Task<TOut> Match<TOut>(Func<TOk, TOut> fOk, Func<TError, TOut> fError) =>
            resultTask.Map(r => r.Match(fOk, fError));

        public async Task<TOut> Match<TOut>(Func<TOk, Task<TOut>> fOk, Func<TError, Task<TOut>> fError) =>
            await resultTask.Map(r => r.Match(fOk, fError));
    }
}
