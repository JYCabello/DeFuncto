using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace DeFuncto.Extensions;

public static class Tuples
{
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<T1, T2, TOut>(this (T1, T2) self, Func<T1, T2, TOut> f) =>
        f(self.Item1, self.Item2);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<T1, T2, T3, TOut>(this (T1, T2, T3) self, Func<T1, T2, T3, TOut> f) =>
        f(self.Item1, self.Item2, self.Item3);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<T1, T2, T3, T4, TOut>(this (T1, T2, T3, T4) self, Func<T1, T2, T3, T4, TOut> f) =>
        f(self.Item1, self.Item2, self.Item3, self.Item4);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<T1, T2, T3, T4, T5, TOut>(this (T1, T2, T3, T4, T5) self, Func<T1, T2, T3, T4, T5, TOut> f) =>
        f(self.Item1, self.Item2, self.Item3, self.Item4, self.Item5);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Apply<T1, T2, T3, T4, T5, T6, TOut>(this (T1, T2, T3, T4, T5, T6) self, Func<T1, T2, T3, T4, T5, T6, TOut> f) =>
        f(self.Item1, self.Item2, self.Item3, self.Item4, self.Item5, self.Item6);
}
