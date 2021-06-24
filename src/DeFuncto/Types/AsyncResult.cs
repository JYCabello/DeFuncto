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

        public static implicit operator AsyncResult<TOk, TError>(Task<Result<TOk, TError>> result) =>
            new(result);

        public Task<Result<TOk, TError>> ToResult() => resultTask;

        public AsyncResult<TOk2, TError> Map<TOk2>(Func<TOk, TOk2> f) =>
            resultTask.Map(r => r.Map(f));

        public AsyncResult<TOk2, TError> Map<TOk2>(Func<TOk, Task<TOk2>> f) =>
            resultTask.Map(r => r.Match(
                ok => f(ok).Map(Ok<TOk2, TError>),
                error => error.Apply(Error<TOk2, TError>).Apply(Task.FromResult)
            ));

        public AsyncResult<TOk, TError2> MapError<TError2>(Func<TError, TError2> f) =>
            resultTask.Map(r => r.MapError(f));

        public AsyncResult<TOk, TError2> MapError<TError2>(Func<TError, Task<TError2>> f) =>
            resultTask.Map(r => r.Match(
                ok => ok.Apply(Ok<TOk, TError2>).Apply(Task.FromResult),
                error => f(error).Map(Error<TOk, TError2>)
            ));

        public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, Result<TOk2, TError>> f) =>
            resultTask.Map(r => r.Bind(f));

        public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, Task<Result<TOk2, TError>>> f) =>
            resultTask.Map(r => r.Match(f, error => error.Apply(Error<TOk2, TError>).Apply(Task.FromResult)));
    }
}
