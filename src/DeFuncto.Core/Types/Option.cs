using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto;

/// <summary>
/// Discriminated union of a value that might be absent.
/// Being Some the present and None the absent case respectively.
/// Biased towards the Some case.
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct Option<T> : IEquatable<Option<T>>
{
    private readonly Du<Unit, T> value;

    /// <summary>
    /// True if it's Some.
    /// </summary>
    public readonly bool IsSome;

    /// <summary>
    /// True if it's None.
    /// </summary>
    public bool IsNone => !IsSome;

    /// <summary>
    /// Constructor for the some case.
    /// </summary>
    /// <param name="value">Value.</param>
    public Option(T value)
    {
        this.value = value;
        IsSome = true;
    }

    private Option(Unit none)
    {
        value = none;
        IsSome = false;
    }

    /// <summary>
    /// Collapses the Option into a single value, coming from the adequate projection.
    /// </summary>
    /// <param name="fSome">Projection for Some.</param>
    /// <param name="fNone">Projection for None.</param>
    /// <typeparam name="TOut">Output type of the projections.</typeparam>
    /// <returns>Projected value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TOut Match<TOut>(Func<T, TOut> fSome, Func<TOut> fNone) =>
        value.Match(_ => fNone(), fSome);

    /// <summary>
    /// Projects the value with a given function.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TOut">Output type of the projection.</typeparam>
    /// <returns>A new Option of type TOut.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<TOut> Map<TOut>(Func<T, TOut> f) =>
        Match(f.Compose(Option<TOut>.Some), () => Option<TOut>.None);

    /// <summary>
    /// Projects the value to an Option with a given function and flattens it.
    /// </summary>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TOut">Value type of the projected function's output.</typeparam>
    /// <returns></returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<TOut> Bind<TOut>(Func<T, Option<TOut>> f) =>
        Map(f).Flatten();

    /// <summary>
    /// Collapses the Option via defaulting.
    /// </summary>
    /// <param name="f">Function generating the default value.</param>
    /// <returns>The collapsed value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T DefaultValue(Func<T> f) =>
        Match(Id, f);

    /// <summary>
    /// Collapses the Option via defaulting.
    /// </summary>
    /// <param name="t">The default value.</param>
    /// <returns>The collapsed value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T DefaultValue(T t) =>
        DefaultValue(() => t);

    /// <summary>
    /// Binds the None state with a provided Option.
    /// </summary>
    /// <param name="opt">The provided option.</param>
    /// <returns>A new Option.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> BindNone(Option<T> opt) =>
        BindNone(() => opt);

    /// <summary>
    /// Binds the None state with a provided Option.
    /// </summary>
    /// <param name="opt">The option provider.</param>
    /// <returns>A new Option.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> BindNone(Func<Option<T>> opt) =>
        Match(Some, opt);

    /// <summary>
    /// Converts the Option into a Result, projecting None into
    /// a provided Error.
    /// </summary>
    /// <param name="f">The error provider.</param>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <returns>A Result.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<T, TError> Result<TError>(Func<TError> f) =>
        Map(Ok<T, TError>).DefaultValue(f.Compose(Error<T, TError>));

    /// <summary>
    /// Converts the Option into a Result, projecting None into
    /// a provided Error.
    /// </summary>
    /// <param name="terror">The error.</param>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <returns>A Result.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<T, TError> Result<TError>(TError terror) =>
        Result(() => terror);

    /// <summary>
    /// Projects the value with a given function.
    /// </summary>
    /// <remarks>
    /// Used to enable LINQ embedded syntax, not meant for direct use.
    /// </remarks>
    /// <param name="f">Projection.</param>
    /// <typeparam name="TOut">Output type of the projection.</typeparam>
    /// <returns>A new Option of type TOut.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<TOut> Select<TOut>(Func<T, TOut> f) => Map(f);

    /// <summary>
    /// Binds and projects the Some state using a binder and a projection
    /// function.
    /// </summary>
    /// <remarks>
    /// Used to enable LINQ embedded syntax, not meant for direct use.
    /// </remarks>
    /// <param name="binder">Binding function.</param>
    /// <param name="projection">Projection.</param>
    /// <typeparam name="TBind">Intermediate type of the binding.</typeparam>
    /// <typeparam name="TFinal">Final type of the projection.</typeparam>
    /// <returns>A new option of the output type.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<TFinal> SelectMany<TBind, TFinal>(
        Func<T, Option<TBind>> binder,
        Func<T, TBind, TFinal> projection
    ) =>
        Bind(t => binder(t).Map(tBind => projection(t, tBind)));

    /// <summary>
    /// Filters the Some state, discarding it if the predicate
    /// is false.
    /// </summary>
    /// <param name="predicate">The predicate to evaluate the value.</param>
    /// <returns>A new Option.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> Where(Func<T, bool> predicate) =>
        this.Apply(self => self.Match(val => predicate(val) ? self : None, () => default));

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fSome">The action to run on some.</param>
    /// <param name="fNone">The action to run on none.</param>
    /// <returns>Unit.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Action<T> fSome, Action fNone) =>
        Match(t =>
            {
                fSome(t);
                return unit;
            },
            () =>
            {
                fNone();
                return unit;
            });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fSome">The action to run on some.</param>
    /// <param name="fNone">The action to run on none.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<T, Unit> fSome, Func<Unit> fNone) =>
        Iter(t => { fSome(t); }, () => { fNone(); });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fNone">The action to run on none.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<Unit> fNone) =>
        Iter(() => { fNone(); });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fNone">The action to run on none.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Action fNone) =>
        Iter(_ => { }, fNone);

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fSome">The action to run on some.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<T, Unit> fSome) =>
        Iter(t => { fSome(t); });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fSome">The action to run on some.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Action<T> fSome) =>
        Iter(fSome, () => { });

    /// <summary>
    /// Factory for the Some state.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>An option in the Some state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Some(T value) => value;

    /// <summary>
    /// Factory for the None state.
    /// </summary>
    public static Option<T> None => new OptionNone();

    /// <summary>
    /// Implicit conversion to Some.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>An option in the Some state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Option<T>(T value) => new(value);

    /// <summary>
    /// Implicit conversion to None.
    /// </summary>
    /// <param name="_">OptionNone to be implicitly converted.</param>
    /// <returns>An option in the None state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Option<T>(OptionNone _) => new(unit);

    public override bool Equals(object obj) =>
        obj is Option<T> other && Equals(other);

    public bool Equals(Option<T> other) =>
        IsSome == other.IsSome && other.value.Equals(value);

    public override int GetHashCode() =>
        -1584136870 + value.GetHashCode();
}

/// <summary>
/// Option in the None state, helpful for implicit conversions.
/// </summary>
public readonly struct OptionNone
{
    /// <summary>
    /// Convert to an Option, giving it a type parameter.
    /// </summary>
    /// <typeparam name="T">The value type of the option.</typeparam>
    /// <returns>An option in the None state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> Option<T>() => DeFuncto.Option<T>.None;
}

public static class OptionExtensions
{
    /// <summary>
    /// Processes a nested Option into an single option.
    /// </summary>
    /// <param name="opt">The nested Option to process.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <returns>The processed option.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Flatten<T>(this Option<Option<T>> opt) =>
        opt.Match(Id, () => Option<T>.None);

    /// <summary>
    /// Wraps an option into its asynchronous version.
    /// </summary>
    /// <param name="opt">The Option.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <returns>The AsyncOption.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncOption<T> Async<T>(this Option<T> opt) => opt;

    /// <summary>
    ///
    /// </summary>
    /// <param name="opt"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncOption<T> Async<T>(this Option<Task<T>> opt) =>
        opt.Match(t => t.Map(Some), () => None.Option<T>().ToTask());

    /// <summary>
    /// Turns an Option containing a Task in an AsyncOption.
    /// </summary>
    /// <param name="opt">The Option containing a Task.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <returns>The AsyncOption.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncOption<T> Async<T>(this Task<Option<T>> opt) => opt;
}
