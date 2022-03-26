using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using DeFuncto.Types;
using static DeFuncto.Prelude;

namespace DeFuncto.Extensions;

/// <summary>
/// Functional extensions to interact with collections using LINQ using DeFuncto
/// data structures.
/// </summary>
public static class Collections
{
    /// <summary>
    /// Out of an Enumerable of Options, filter and map it to an Enumerable
    /// with the values of the Options in the Some state.
    /// </summary>
    /// <param name="self">An enumerable of Options.</param>
    /// <typeparam name="T">Type of the value of the Options.</typeparam>
    /// <returns>An Enumerable with the values of the Options in the Some state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Choose<T>(this IEnumerable<Option<T>> self) =>
        self.Where(opt => opt.IsSome).Select(opt => opt.Match(Id, () => default!));

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<TOut> Choose<T, TOut>(this IEnumerable<T> self, Func<T, Option<TOut>> f) =>
        self.Select(f).Choose();

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> self) =>
        self.Select(t => new Box<T>(t)).FirstOrDefault().Apply(Optional).Map(box => box.Value);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> FirstOrNone<T>(this List<T> self) =>
        self.Select(t => new Box<T>(t)).FirstOrDefault().Apply(Optional).Map(box => box.Value);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> self, Func<T, bool> filter) =>
        self.Where(filter).FirstOrNone();

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> FirstOrNone<T>(this List<T> self, Func<T, bool> filter) =>
        self.Where(filter).FirstOrNone();

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> SingleOrNone<T>(this IEnumerable<T> self) =>
        self.Select(t => new Box<T>(t)).SingleOrDefault().Apply(Optional).Map(box => box.Value);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> SingleOrNone<T>(this List<T> self) =>
        self.Select(t => new Box<T>(t)).SingleOrDefault().Apply(Optional).Map(box => box.Value);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> SingleOrNone<T>(this IEnumerable<T> self, Func<T, bool> filter) =>
        self.Where(filter).SingleOrNone();

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> SingleOrNone<T>(this List<T> self, Func<T, bool> filter) =>
        self.Where(filter).SingleOrNone();
}
