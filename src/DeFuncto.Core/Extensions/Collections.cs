﻿using System;
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
    /// Taking an Enumerable of type Option T, get only the present values in a new Enumerable of type T.
    /// </summary>
    /// <param name="self">An enumerable of Options.</param>
    /// <typeparam name="T">Value type of the Options.</typeparam>
    /// <returns>An Enumerable with the values of the Options in the Some state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Choose<T>(this IEnumerable<Option<T>> self) =>
        self.Where(opt => opt.IsSome).Select(opt => opt.Match(Id, () => default!));

    /// <summary>
    /// Taking an Enumerable of type T, apply a function to every element to an Option of type T,
    /// then get only the present values in a new Enumerable of type T.
    /// </summary>
    /// <param name="self">The Enumerable to process.</param>
    /// <param name="f">Projection function.</param>
    /// <typeparam name="T">Input type.</typeparam>
    /// <typeparam name="TOut">Value type of the Option in the projection.</typeparam>
    /// <returns>An Enumerable with the values of the Options with a present value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<TOut> Choose<T, TOut>(this IEnumerable<T> self, Func<T, Option<TOut>> f) =>
        self.Select(f).Choose();

    /// <summary>
    /// Gets the first element of an Enumerable as Some, or None if there was no value.
    /// </summary>
    /// <param name="self">The Enumerable to get the first element from.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>An Option with the first item or None.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> self) =>
        self.Select(t => new Box<T>(t)).FirstOrDefault().Apply(Optional).Map(box => box.Value);

    /// <summary>
    /// Gets the first element of an Enumerable that matches the filter predicate or None if
    /// no element matching is found.
    /// </summary>
    /// <param name="self">The Enumerable to get the first matching element from.</param>
    /// <param name="filter">The filter predicate.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>An Option with the first matching element or None.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> self, Func<T, bool> filter) =>
        self.Where(filter).FirstOrNone();

    /// <summary>
    /// Gets the only element in an Enumerable in an Option or None if it's empty or it has
    /// more than one element.
    /// </summary>
    /// <param name="self">The Enumerable to get a single element from.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>Some if there was a single element, None otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> SingleOrNone<T>(this IEnumerable<T> self) =>
        self.Select(t => new Box<T>(t)).SingleOrDefault().Apply(Optional).Map(box => box.Value);

    /// <summary>
    /// If there's a single element in an Enumerable matching the filter predicate, return it
    /// in an Option, None otherwise.
    /// </summary>
    /// <param name="self">The Enumerable to filter a single element from.</param>
    /// <param name="filter">The predicate to filter.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>Some if there was a single element matching the predicate, None otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> SingleOrNone<T>(this IEnumerable<T> self, Func<T, bool> filter) =>
        self.Where(filter).SingleOrNone();
}
