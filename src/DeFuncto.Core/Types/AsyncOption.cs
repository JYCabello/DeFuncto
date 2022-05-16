using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto;

/// <summary>
/// Discriminated union representing an asynchronous value that might be absent.
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

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<TOut> Bind<TOut>(Func<T, Task<Option<TOut>>> f) =>
        Bind(f.Compose(OptionExtensions.Async));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(Option<T> opt) =>
        BindNone(() => opt);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(Func<Option<T>> fOption) =>
        Match(Some, fOption);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(Task<Option<T>> taskOption) =>
        BindNone(() => taskOption);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(Func<Task<Option<T>>> fTaskOption) =>
        Match(val => Some(val).ToTask(), fTaskOption);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(AsyncOption<T> asyncOption) =>
        BindNone(() => asyncOption);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> BindNone(Func<AsyncOption<T>> fAsyncOption) =>
        Match(val => Some(val).ToTask(), () => fAsyncOption().Option);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<T> DefaultValue(T t) =>
        DefaultValue(() => t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<T> DefaultValue(Func<T> f) =>
        DefaultValue(f.Compose(Task.FromResult));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<T> DefaultValue(Task<T> task) =>
        DefaultValue(() => task);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<T> DefaultValue(Func<Task<T>> f) =>
        Match(Prelude.Compose<T, T, Task<T>>(Id, Task.FromResult), f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<TOut> Select<TOut>(Func<T, TOut> f) => Map(f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<TFinal> SelectMany<TBind, TFinal>(
        Func<T, AsyncOption<TBind>> binder,
        Func<T, TBind, TFinal> projection
    ) =>
        Bind(t => binder(t).Map(tBind => projection(t, tBind)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncOption<T> Where(Func<T, bool> predicate) =>
        Option.Map(opt => opt.Where(predicate));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Action<T> f) =>
        Option.Map(opt => opt.Iter(f));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<Unit> f) =>
        Iter(f.Action());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Unit> f) =>
        Iter(f.Action());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Task> f) =>
        Option.Map(async opt =>
        {
            await opt.Match(f, () => Task.CompletedTask);
            return unit;
        });

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Task<Unit>> f) =>
        Iter(f.AsyncAction());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Action f) =>
        Option.Map(opt => opt.Iter(f));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<Task> f) =>
        Option.Map(async opt =>
        {
            await opt.Match(_ => Task.CompletedTask, f);
            return unit;
        });

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<Task<Unit>> f) =>
        Iter(f.AsyncAction());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Action<T> fSome, Action fNone) =>
        Iter(fSome.Function(), fNone.Function());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Unit> fSome, Func<Unit> fNone) =>
        Match(fSome, fNone);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Task> fSome, Func<Task> fNone) =>
        Match(fSome.AsyncFunction(), fNone.AsyncFunction());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Unit> Iter(Func<T, Task<Unit>> fSome, Func<Task<Unit>> fNone) =>
        Iter(fSome.AsyncAction(), fNone.AsyncAction());

    private readonly Task<Option<T>>? option;
    public Task<Option<T>> Option => option ?? Task.FromResult(None.Option<T>());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<T, TError> Result<TError>(Func<Task<TError>> fError) =>
        Match(t => Ok<T, TError>(t).ToTask(), () => fError().Map(Error<T, TError>));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<T, TError> Result<TError>(Func<TError> fError) =>
        fError.Compose(Task.FromResult).Apply(Result);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AsyncResult<T, TError> Result<TError>(TError error) =>
        Result(() => error);

    public static implicit operator AsyncOption<T>(T val) => new(val);
    public static implicit operator AsyncOption<T>(Option<T> option) => new(option);
    public static implicit operator AsyncOption<T>(Task<Option<T>> option) => new(option);
}

public static class AsyncOptionExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncOption<T> Flatten<T>(this AsyncOption<AsyncOption<T>> self) =>
        self.Match(t => t.Option, () => None.Option<T>().ToTask());
}
