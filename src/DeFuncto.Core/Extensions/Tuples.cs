using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace DeFuncto.Extensions;

/// <summary>
/// Extensions to treat tuples as functors.
/// </summary>
public static class Tuples
{
    /// <summary>
    /// Applies a function to the tuple, facilitating the deconstruction by
    /// passing the elements of the tuple to the function.
    /// </summary>
    /// <param name="self">The tuple.</param>
    /// <param name="f">Projection function.</param>
    /// <typeparam name="T1">Type of the first element of the tuple.</typeparam>
    /// <typeparam name="T2">Type of the second element of the tuple.</typeparam>
    /// <typeparam name="TOut">Output type of the projection.</typeparam>
    /// <returns>The output of the projection.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<T1, T2, TOut>(
        this (T1, T2) self,
        Func<T1, T2, TOut> f
    ) =>
        f(self.Item1, self.Item2);

    /// <summary>
    /// Applies a function to the tuple, facilitating the deconstruction by
    /// passing the elements of the tuple to the function.
    /// </summary>
    /// <param name="self">The tuple.</param>
    /// <param name="f">Projection function.</param>
    /// <typeparam name="T1">Type of the first element of the tuple.</typeparam>
    /// <typeparam name="T2">Type of the second element of the tuple.</typeparam>
    /// <typeparam name="T3">Type of the third element of the tuple.</typeparam>
    /// <typeparam name="TOut">Output type of the projection.</typeparam>
    /// <returns>The output of the projection.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<T1, T2, T3, TOut>(
        this (T1, T2, T3) self,
        Func<T1, T2, T3, TOut> f
    ) =>
        f(self.Item1, self.Item2, self.Item3);

    /// <summary>
    /// Applies a function to the tuple, facilitating the deconstruction by
    /// passing the elements of the tuple to the function.
    /// </summary>
    /// <param name="self">The tuple.</param>
    /// <param name="f">Projection function.</param>
    /// <typeparam name="T1">Type of the first element of the tuple.</typeparam>
    /// <typeparam name="T2">Type of the second element of the tuple.</typeparam>
    /// <typeparam name="T3">Type of the third element of the tuple.</typeparam>
    /// <typeparam name="T4">Type of the fourth element of the tuple.</typeparam>
    /// <typeparam name="TOut">Output type of the projection.</typeparam>
    /// <returns>The output of the projection.</returns>

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<T1, T2, T3, T4, TOut>(
        this (T1, T2, T3, T4) self,
        Func<T1, T2, T3, T4, TOut> f
    ) =>
        f(self.Item1, self.Item2, self.Item3, self.Item4);

    /// <summary>
    /// Applies a function to the tuple, facilitating the deconstruction by
    /// passing the elements of the tuple to the function.
    /// </summary>
    /// <param name="self">The tuple.</param>
    /// <param name="f">Projection function.</param>
    /// <typeparam name="T1">Type of the first element of the tuple.</typeparam>
    /// <typeparam name="T2">Type of the second element of the tuple.</typeparam>
    /// <typeparam name="T3">Type of the third element of the tuple.</typeparam>
    /// <typeparam name="T4">Type of the fourth element of the tuple.</typeparam>
    /// <typeparam name="T5">Type of the fifth element of the tuple.</typeparam>
    /// <typeparam name="TOut">Output type of the projection.</typeparam>
    /// <returns>The output of the projection.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<T1, T2, T3, T4, T5, TOut>(
        this (T1, T2, T3, T4, T5) self,
        Func<T1, T2, T3, T4, T5, TOut> f
    ) =>
        f(self.Item1, self.Item2, self.Item3, self.Item4, self.Item5);

    /// <summary>
    /// Applies a function to the tuple, facilitating the deconstruction by
    /// passing the elements of the tuple to the function.
    /// </summary>
    /// <param name="self">The tuple.</param>
    /// <param name="f">Projection function.</param>
    /// <typeparam name="T1">Type of the first element of the tuple.</typeparam>
    /// <typeparam name="T2">Type of the second element of the tuple.</typeparam>
    /// <typeparam name="T3">Type of the third element of the tuple.</typeparam>
    /// <typeparam name="T4">Type of the fourth element of the tuple.</typeparam>
    /// <typeparam name="T5">Type of the fifth element of the tuple.</typeparam>
    /// <typeparam name="T6">Type of the sixth element of the tuple.</typeparam>
    /// <typeparam name="TOut">Output type of the projection.</typeparam>
    /// <returns>The output of the projection.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<T1, T2, T3, T4, T5, T6, TOut>(
        this (T1, T2, T3, T4, T5, T6) self,
        Func<T1, T2, T3, T4, T5, T6, TOut> f
    ) =>
        f(self.Item1, self.Item2, self.Item3, self.Item4, self.Item5, self.Item6);
}
