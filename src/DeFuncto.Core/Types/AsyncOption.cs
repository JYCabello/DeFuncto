using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto;

/// <summary>
/// Discriminated union representing an asynchronous value that might be absent.
/// Biased towards the present case, most operations act on it.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public readonly struct AsyncOption<T>
{
    /// <summary>
    /// True if the value is present.
    /// </summary>
    public Task<bool> IsSome => Option.Map(opt => opt.IsSome);

    /// <summary>
    /// True if the value is absent.
    /// </summary>
    public Task<bool> IsNone => Option.Map(opt => opt.IsNone);

    /// <summary>
    /// Constructs an async option from a synchronous one.
    /// </summary>
    /// <param name="option">The synchronous option.</param>
    public AsyncOption(Option<T> option) : this(option.ToTask()) { }

    /// <summary>
    /// Constructs an async option from a task with an option result.
    /// </summary>
    /// <param name="optionTask">The task with an option result.</param>
    public AsyncOption(Task<Option<T>> optionTask) =>
        option = optionTask;

    /// <summary>
    /// Takes one function for the absent case and one for the present
    /// and executes only the according one.
    /// </summary>
    /// <param name="fSome">Projection for the present case.</param>
    /// <param name="fNone">Projection for the absent case.</param>
    /// <typeparam name="TOut">Output type of the projections.</typeparam>
    /// <returns>A task returning the output of  the corresponding projection.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<TOut> Match<TOut>(Func<T, TOut> fSome, Func<TOut> fNone) =>
        Match(fSome.Compose(Task.FromResult), fNone.Compose(Task.FromResult));

    /// <summary>
    /// Takes one function for the absent case and one for the present
    /// and executes only the according one.
    /// </summary>
    /// <param name="fSome">Projection for the present case.</param>
    /// <param name="fNone">Projection for the absent case.</param>
    /// <typeparam name="TOut">Output type of the projections.</typeparam>
    /// <returns>A task returning the output of  the corresponding projection.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<TOut> Match<TOut>(Func<T, Task<TOut>> fSome, Func<Task<TOut>> fNone) =>
        Option.Map(opt => opt.Match(fSome, fNone));

    /// <summary>
    /// Projects the value if it's present, using the provided projection.
    /// </summary>
    /// <param name="f">The projection.</param>
    /// <typeparam name="TOut">Output type of the projection.</typeparam>
    /// <returns>A new async option of the output type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<TOut> Map<TOut>(Func<T, TOut> f) =>
        Option.Map(opt => opt.Map(f));

    /// <summary>
    /// Projects the value if it's present, using the provided projection.
    /// </summary>
    /// <param name="f">The projection.</param>
    /// <typeparam name="TOut">Output type of the projection.</typeparam>
    /// <returns>A new async option of the output type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<TOut> Map<TOut>(Func<T, Task<TOut>> f) =>
        Match(
            t => f(t).Map(Some),
            () => Option<TOut>.None.ToTask()
        );

    /// <summary>
    /// Projects the value to an option and flattens it.
    /// </summary>
    /// <param name="f">The projection.</param>
    /// <typeparam name="TOut">
    /// The output type of the projected asynchronous option.
    /// </typeparam>
    /// <returns>A new async option of the output type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<TOut> Bind<TOut>(Func<T, AsyncOption<TOut>> f) =>
        Map(f).Flatten();

    /// <summary>
    /// Projects the value to an option and flattens it.
    /// </summary>
    /// <param name="f">The projection.</param>
    /// <typeparam name="TOut">
    /// The output type of the projected option.
    /// </typeparam>
    /// <returns>A new async option of the output type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<TOut> Bind<TOut>(Func<T, Option<TOut>> f) =>
        Bind(f.Compose(OptionExtensions.Async));

    /// <summary>
    /// Projects the value to an option and flattens it.
    /// </summary>
    /// <param name="f">The projection.</param>
    /// <typeparam name="TOut">
    /// The output type of the projected option.
    /// </typeparam>
    /// <returns>A new async option of the output type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<TOut> Bind<TOut>(Func<T, Task<Option<TOut>>> f) =>
        Bind(f.Compose(OptionExtensions.Async));

    /// <summary>
    /// Binds the absent state to an option.
    /// </summary>
    /// <param name="opt">The option to bind.</param>
    /// <returns>A new async option.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(Option<T> opt) =>
        BindNone(() => opt);

    /// <summary>
    /// Binds the absent state to an option.
    /// </summary>
    /// <param name="fOption">A function producing the option to bind.</param>
    /// <returns>A new async option.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(Func<Option<T>> fOption) =>
        Match(Some, fOption);

    /// <summary>
    /// Binds the absent state to an option.
    /// </summary>
    /// <param name="taskOption">A task resulting in an option to bind.</param>
    /// <returns>A new async option.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(Task<Option<T>> taskOption) =>
        BindNone(() => taskOption);

    /// <summary>
    /// Binds the absent state to an option.
    /// </summary>
    /// <param name="fTaskOption">
    /// An asynchronous function producing the option to bind.
    /// </param>
    /// <returns>A new async option.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(Func<Task<Option<T>>> fTaskOption) =>
        Match(val => Some(val).ToTask(), fTaskOption);

    /// <summary>
    /// Binds the absent state to an option.
    /// </summary>
    /// <param name="asyncOption">An asynchronous option to bind.</param>
    /// <returns>A new async option.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(AsyncOption<T> asyncOption) =>
        BindNone(() => asyncOption);

    /// <summary>
    /// Binds the absent state to an option.
    /// </summary>
    /// <param name="fAsyncOption">
    /// A function producing an asynchronous option to bind
    /// </param>
    /// <returns>A new async option.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(Func<AsyncOption<T>> fAsyncOption) =>
        Match(val => Some(val).ToTask(), () => fAsyncOption().Option);

    /// <summary>
    /// Collapses the option into an output via defaulting
    /// the absent case.
    /// </summary>
    /// <param name="t">The default value.</param>
    /// <returns>A task returning the collapsed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<T> DefaultValue(T t) =>
        DefaultValue(() => t);

    /// <summary>
    /// Collapses the option into an output via defaulting
    /// the absent case.
    /// </summary>
    /// <param name="f">A function producing the default value.</param>
    /// <returns>A task returning the collapsed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<T> DefaultValue(Func<T> f) =>
        DefaultValue(f.Compose(Task.FromResult));

    /// <summary>
    /// Collapses the option into an output via defaulting
    /// the absent case.
    /// </summary>
    /// <param name="task">A function returning the default value.</param>
    /// <returns>A task returning the collapsed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<T> DefaultValue(Task<T> task) =>
        DefaultValue(() => task);

    /// <summary>
    /// Collapses the option into an output via defaulting
    /// the absent case.
    /// </summary>
    /// <param name="f">
    /// An asynchronous function returning the default value.
    /// </param>
    /// <returns>A task returning the collapsed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<T> DefaultValue(Func<Task<T>> f) =>
        Match(Prelude.Compose<T, T, Task<T>>(Id, Task.FromResult), f);

    /// <summary>
    /// Projects the value if it's present, using the provided projection.
    /// </summary>
    /// <remarks>
    /// Used to enable LINQ embedded syntax, not meant for direct use.
    /// </remarks>
    /// <param name="f">The projection.</param>
    /// <typeparam name="TOut">Output type of the projection.</typeparam>
    /// <returns>A new async option of the output type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<TOut> Select<TOut>(Func<T, TOut> f) => Map(f);

    /// <summary>
    /// Binds and projects the present state using a binder and a projection
    /// function.
    /// </summary>
    /// <remarks>
    /// Used to enable LINQ embedded syntax, not meant for direct use.
    /// </remarks>
    /// <param name="binder">Binding function.</param>
    /// <param name="projection">Projection.</param>
    /// <typeparam name="TBind">Intermediate type of the binding.</typeparam>
    /// <typeparam name="TFinal">Final type of the projection.</typeparam>
    /// <returns>A new async option of the output type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<TFinal> SelectMany<TBind, TFinal>(
        Func<T, AsyncOption<TBind>> binder,
        Func<T, TBind, TFinal> projection
    ) =>
        Bind(t => binder(t).Map(tBind => projection(t, tBind)));

    /// <summary>
    /// Filters the present state, discarding it if the predicate
    /// is false.
    /// </summary>
    /// <param name="predicate">The predicate to evaluate the value.</param>
    /// <returns>A new async option.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> Where(Func<T, bool> predicate) =>
        Option.Map(opt => opt.Where(predicate));

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="f">The action.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Action<T> f) =>
        Option.Map(opt => opt.Iter(f));

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="f">The action.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<Unit> f) =>
        Iter(f.Action());

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="f">The action.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Unit> f) =>
        Iter(f.Action());

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="f">The action.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Task> f) =>
        Option.Map(async opt =>
        {
            await opt.Match(f, () => Task.CompletedTask);
            return unit;
        });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="f">The action.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Task<Unit>> f) =>
        Iter(f.AsyncAction());

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="f">The action.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Action f) =>
        Option.Map(opt => opt.Iter(f));

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="f">The action.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<Task> f) =>
        Option.Map(async opt =>
        {
            await opt.Match(_ => Task.CompletedTask, f);
            return unit;
        });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="f">The action.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<Task<Unit>> f) =>
        Iter(f.AsyncAction());

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fSome">The action for the present state.</param>
    /// <param name="fNone">The action for the present state.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Action<T> fSome, Action fNone) =>
        Iter(fSome.Function(), fNone.Function());

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fSome">The action for the present state.</param>
    /// <param name="fNone">The action for the present state.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Unit> fSome, Func<Unit> fNone) =>
        Match(fSome, fNone);

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fSome">The action for the present state.</param>
    /// <param name="fNone">The action for the present state.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Task> fSome, Func<Task> fNone) =>
        Match(fSome.AsyncFunction(), fNone.AsyncFunction());

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fSome">The action for the present state.</param>
    /// <param name="fNone">The action for the present state.</param>
    /// <returns>A task with a Unit result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Task<Unit>> fSome, Func<Task<Unit>> fNone) =>
        Iter(fSome.AsyncAction(), fNone.AsyncAction());

    private readonly Task<Option<T>>? option;

    /// <summary>
    /// Removes the wrapper for the asynchrony, giving a task with an option
    /// as a result.
    /// </summary>
    public Task<Option<T>> Option => option ?? Task.FromResult(None.Option<T>());

    /// <summary>
    /// Converts to an AsyncResult, mapping the present state to Ok and the absent
    /// to a provided Error.
    /// </summary>
    /// <see cref="AsyncResult{TOk,TError}" />
    /// <param name="fError">The asynchronous function providing the error.</param>
    /// <typeparam name="TError">Error type for the Result.</typeparam>
    /// <returns>An async result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<T, TError> Result<TError>(Func<Task<TError>> fError) =>
        Match(t => Ok<T, TError>(t).ToTask(), () => fError().Map(Error<T, TError>));

    /// <summary>
    /// Converts to an AsyncResult, mapping the present state to Ok and the absent
    /// to a provided Error.
    /// </summary>
    /// <param name="fError">The synchronous function providing the error.</param>
    /// <typeparam name="TError">Error type for the Result.</typeparam>
    /// <returns>An async result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<T, TError> Result<TError>(Func<TError> fError) =>
        fError.Compose(Task.FromResult).Apply(Result);

    /// <summary>
    /// Converts to an AsyncResult, mapping the present state to Ok and the absent
    /// to a provided Error.
    /// </summary>
    /// <param name="error">Provided error.</param>
    /// <typeparam name="TError">Error type for the Result.</typeparam>
    /// <returns>An async result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<T, TError> Result<TError>(TError error) =>
        Result(() => error);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator AsyncOption<T>(T val) => new(val);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator AsyncOption<T>(Option<T> option) => new(option);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator AsyncOption<T>(Task<Option<T>> option) => new(option);
}

public static class AsyncOptionExtensions
{
    /// <summary>
    /// Flattens two nested AsyncOptions.
    /// </summary>
    /// <param name="self">Nested AsyncOptions to flatten.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>A flattened AsyncOption.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncOption<T> Flatten<T>(this AsyncOption<AsyncOption<T>> self) =>
        self.Match(t => t.Option, () => None.Option<T>().ToTask());
}
