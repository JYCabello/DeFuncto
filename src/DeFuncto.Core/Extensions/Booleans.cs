using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DeFuncto.Extensions;

/// <summary>
/// Extensions to treat booleans as discriminated unions.
/// </summary>
public static class Booleans
{
    /// <summary>
    /// Performs a ternary operation on the value of a boolean
    /// and returns the result of the appropriate function.
    /// </summary>
    /// <param name="boolean">The boolean to match on.</param>
    /// <param name="onTrue">Function to run on true.</param>
    /// <param name="onFalse">Function to run on false.</param>
    /// <typeparam name="TOut">Return type common to both functions.</typeparam>
    /// <returns>The result of the function that gets run.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Match<TOut>(this bool boolean, Func<TOut> onTrue, Func<TOut> onFalse) =>
        boolean ? onTrue() : onFalse();

    /// <summary>
    /// Matches on a task returning a boolean as if the boolean was a discriminated union.
    /// </summary>
    /// <param name="boolean">The asynchronous boolean to match on.</param>
    /// <param name="onTrue">Function to run on true.</param>
    /// <param name="onFalse">Function to run on false.</param>
    /// <typeparam name="TOut">Return type common to both functions.</typeparam>
    /// <returns>A Task with the result of the function that gets run.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<TOut> Match<TOut>(this Task<bool> boolean, Func<TOut> onTrue, Func<TOut> onFalse) =>
        boolean.Map(b => b.Match(onTrue, onFalse));

    /// <summary>
    /// Matches on a task returning boolean with two asynchronous functions as if the boolean was a discriminated union.
    /// </summary>
    /// <param name="boolean">The asynchronous boolean to match on.</param>
    /// <param name="onTrue">Asynchronous function to run on true.</param>
    /// <param name="onFalse">Asynchronous function to run on false.</param>
    /// <typeparam name="TOut">Return type common to both functions.</typeparam>
    /// <returns>A Task with the result of the function that gets run.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<TOut> Match<TOut>(this Task<bool> boolean, Func<Task<TOut>> onTrue, Func<Task<TOut>> onFalse) =>
        boolean.Map(b => b.Match(onTrue, onFalse));
}
