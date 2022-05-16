using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto;

/// <summary>
/// Discriminated union representing the result of an asynchronous operation,
/// with an Ok state for the success and an Error state for the failure.
/// Biased towards the Ok case.
/// </summary>
/// <typeparam name="TOk">Error type.</typeparam>
/// <typeparam name="TError">Value type.</typeparam>
public readonly struct AsyncResult<TOk, TError>
{
    private readonly Task<Result<TOk, TError>> resultTask;

    /// <summary>
    /// Constructor from a synchronous result.
    /// </summary>
    /// <param name="result">The synchronous result.</param>
    public AsyncResult(Result<TOk, TError> result) : this(Task.FromResult(result)) { }

    /// <summary>
    /// Constructor from a task.
    /// </summary>
    /// <param name="resultTask">A task that has a synchronous result as a result.</param>
    public AsyncResult(Task<Result<TOk, TError>> resultTask) =>
        this.resultTask = resultTask;

    /// <summary>
    /// Unwraps the asynchronous abstraction, returning a task.
    /// </summary>
    /// <returns>A task with the result as a result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Result<TOk, TError>> ToTask() => resultTask;

    /// <summary>
    /// Projects the Ok value.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TOk2">New value type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk2, TError> Map<TOk2>(Func<TOk, TOk2> f) =>
        resultTask.Map(r => r.Map(f));

    /// <summary>
    /// Projects the Ok value.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TOk2">New value type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk2, TError> Map<TOk2>(Func<TOk, Task<TOk2>> f) =>
        Match(
            ok => f(ok).Map(Ok<TOk2, TError>),
            error => error.Apply(Error<TOk2, TError>).ToTask()
        );

    /// <summary>
    /// Projects the error value.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TError2">New error type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk, TError2> MapError<TError2>(Func<TError, TError2> f) =>
        Match(Ok<TOk, TError2>, e => f(e));

    /// <summary>
    /// Projects the error value.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TError2">New error type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk, TError2> MapError<TError2>(Func<TError, Task<TError2>> f) =>
        Match(
            ok => ok.Apply(Ok<TOk, TError2>).ToTask(),
            error => f(error).Map(Error<TOk, TError2>)
        );

    /// <summary>
    /// Projects the Ok value to another async result with the same Error type and
    /// flattens the result.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TOk2">Projected type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, Result<TOk2, TError>> f) =>
        Bind(ok => f(ok).Async());

    /// <summary>
    /// Projects the Ok value to another async result with the same Error type and
    /// flattens the result.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TOk2">Projected type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, Task<Result<TOk2, TError>>> f) =>
        Bind(ok => f(ok).Async());

    /// <summary>
    /// Projects the Ok value to another async result with the same Error type and
    /// flattens the result.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TOk2">Projected type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk2, TError> Bind<TOk2>(Func<TOk, AsyncResult<TOk2, TError>> f) =>
        Map(f).Flatten();

    /// <summary>
    /// Projects the Error value to another async result with the same Ok type and
    /// flattens the result.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TError2">Projected Error type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk, TError2> BindError<TError2>(Func<TError, Result<TOk, TError2>> f) =>
        BindError(error => f(error).Async());

    /// <summary>
    /// Projects the Error value to another async result with the same Ok type and
    /// flattens the result.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TError2">Projected Error type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk, TError2> BindError<TError2>(Func<TError, Task<Result<TOk, TError2>>> f) =>
        BindError(error => f(error).Async());

    /// <summary>
    /// Projects the Error value to another async result with the same Ok type and
    /// flattens the result.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TError2">Projected Error type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk, TError2> BindError<TError2>(Func<TError, AsyncResult<TOk, TError2>> f) =>
        MapError(f).Flatten();

    /// <summary>
    /// Collapses the structure in an output value, choosing the adequate projection
    /// for each of the states.
    /// </summary>
    /// <param name="fOk">Value projection.</param>
    /// <param name="fError">Error projection.</param>
    /// <typeparam name="TOut">Projected type.</typeparam>
    /// <returns>The correct projection output.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<TOut> Match<TOut>(Func<TOk, TOut> fOk, Func<TError, TOut> fError) =>
        resultTask.Map(r => r.Match(fOk, fError));

    /// <summary>
    /// Collapses the structure in an output value, choosing the adequate projection
    /// for each of the states.
    /// </summary>
    /// <param name="fOk">Value projection.</param>
    /// <param name="fError">Error projection.</param>
    /// <typeparam name="TOut">Projected type.</typeparam>
    /// <returns>The correct projection output.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<TOut> Match<TOut>(Func<TOk, Task<TOut>> fOk, Func<TError, Task<TOut>> fError) =>
        resultTask.Map(r => r.Match(fOk, fError));

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fOk">Value effectful function.</param>
    /// <param name="fError">Error effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<TOk, Task<Unit>> fOk, Func<TError, Task<Unit>> fError) =>
        Iter(async ok => { await fOk(ok); }, async error => { await fError(error); });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fOk">Value effectful function.</param>
    /// <param name="fError">Error effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<TOk, Task> fOk, Func<TError, Task> fError) =>
        Match(
            async ok =>
            {
                await fOk(ok);
                return unit;
            },
            async error =>
            {
                await fError(error);
                return unit;
            }
        );

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fOk">Value effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<TOk, Task<Unit>> fOk) =>
        Iter(fOk, _ => unit.ToTask());

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fOk">Value effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<TOk, Task> fOk) =>
        Iter(fOk, _ => unit.ToTask());

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fOk">Value effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<TOk, Unit> fOk) =>
        Iter(ok => fOk(ok).ToTask());

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fOk">Value effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Action<TOk> fOk) =>
        Iter(ok =>
        {
            fOk(ok);
            return unit;
        });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fError">Error effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<TError, Task<Unit>> fError) =>
        Iter(_ => unit.ToTask(), fError);

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fError">Error effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<TError, Task> fError) =>
        Iter(_ => unit.ToTask(), fError);

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fError">Error effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<TError, Unit> fError) =>
        Iter(ok => fError(ok).ToTask());

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fError">Error effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Action<TError> fError) =>
        Iter(ok =>
        {
            fError(ok);
            return unit;
        });

    /// <summary>
    /// Projects the Ok value.
    /// </summary>
    /// <remarks>
    /// Used to enable LINQ embedded syntax, not meant for direct use.
    /// </remarks>
    /// <param name="projection">Projection.</param>
    /// <typeparam name="TOk2">New value type.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOk2, TError> Select<TOk2>(Func<TOk, TOk2> projection) => Map(projection);


    /// <summary>
    /// Binds and projects the present state using a binder and a projection
    /// function.
    /// </summary>
    /// <remarks>
    /// Used to enable LINQ embedded syntax, not meant for direct use.
    /// </remarks>
    /// <param name="binder">Binding function.</param>
    /// <param name="projection">Projection.</param>
    /// <typeparam name="TOkBind">Intermediate type of the binding.</typeparam>
    /// <typeparam name="TOkFinal">Final type of the projection.</typeparam>
    /// <returns>A new AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<TOkFinal, TError> SelectMany<TOkBind, TOkFinal>(
        Func<TOk, AsyncResult<TOkBind, TError>> binder,
        Func<TOk, TOkBind, TOkFinal> projection
    ) =>
        Bind(ok => binder(ok).Map(okbind => (ok, okbind)))
            .Match(
                okTpl => Ok<TOkFinal, TError>(projection(okTpl.ok, okTpl.okbind)),
                Error<TOkFinal, TError>
            );

    /// <summary>
    /// True if it's Ok.
    /// </summary>
    public Task<bool> IsOk => resultTask.Map(r => r.IsOk);

    /// <summary>
    /// True if it's Error.
    /// </summary>
    public Task<bool> IsError => resultTask.Map(r => r.IsError);

    /// <summary>
    /// An option, Some if Ok, None if Error.
    /// </summary>
    public AsyncOption<TOk> Option => Match(Some, _ => None);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator AsyncResult<TOk, TError>(Task<Result<TOk, TError>> result) =>
        new(result);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator AsyncResult<TOk, TError>(Task<TOk> ok) =>
        ok.Map(Ok<TOk, TError>);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator AsyncResult<TOk, TError>(Task<TError> error) =>
        error.Map(Error<TOk, TError>);
}

public static class AsyncResultExtensions
{
    /// <summary>
    /// For Ok and Error being the same type, return the corresponding value. 
    /// </summary>
    /// <param name="self">The AsyncResult.</param>
    /// <typeparam name="T">Type of both Ok and Error.</typeparam>
    /// <returns>The value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<T> Collapse<T>(this AsyncResult<T, T> self) =>
        self.Match(Id, Id);

    /// <summary>
    /// Flattens two nested AsyncResults.
    /// </summary>
    /// <param name="self">Nested AsyncResults.</param>
    /// <typeparam name="TOk">Ok type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <returns>Flattened AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncResult<TOk, TError> Flatten<TOk, TError>(this AsyncResult<TOk, AsyncResult<TOk, TError>> self) =>
        self.Match(ok => Ok<TOk, TError>(ok).ToTask(), error => error.ToTask());

    /// <summary>
    /// Flattens two nested AsyncResults.
    /// </summary>
    /// <param name="self">Nested AsyncResults.</param>
    /// <typeparam name="TOk">Ok type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <returns>Flattened AsyncResult.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncResult<TOk, TError> Flatten<TOk, TError>(this AsyncResult<AsyncResult<TOk, TError>, TError> self) =>
        self.Match(ok => ok.ToTask(), error => Error<TOk, TError>(error).ToTask());
}
