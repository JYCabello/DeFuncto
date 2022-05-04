using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static DeFuncto.Prelude;

namespace DeFuncto.Extensions;

/// <summary>
/// Extension methods to apply functions to any kind of object.
/// </summary>
public static class Objects
{
    /// <summary>
    /// Apply a function to an object.
    /// </summary>
    /// <param name="self">The object to apply the function to.</param>
    /// <param name="f">The function to apply.</param>
    /// <typeparam name="TIn">Type of the object we'll apply the function to.</typeparam>
    /// <typeparam name="TOut">Output type of the function.</typeparam>
    /// <returns>The output of the applied function.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<TIn, TOut>(this TIn self, Func<TIn, TOut> f) => f(self);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Unit Run<T>(this T self, Func<T, Unit> f) => self.Apply(f).Apply(_ => Unit.Default);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Unit Run<T>(this T self, Action<T> f) => self.Apply(t =>
    {
        f(t);
        return Unit.Default;
    });

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<Unit> Run<T>(this Task<T> self, Action<T> f) => self.Map(t =>
    {
        f(t);
        return Unit.Default;
    });

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> When<T>(this T self, Func<T, bool> predicate) =>
        predicate(self) ? Some(self) : None;
}
