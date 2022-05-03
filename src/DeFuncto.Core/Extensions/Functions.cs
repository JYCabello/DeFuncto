using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static DeFuncto.Prelude;

namespace DeFuncto.Extensions;

/// <summary>
/// Helpers to alter the nature of Functions.
/// </summary>
public static class Functions
{
    /// <summary>
    /// Converts a Parameterless function that returns a value into an Action.
    /// </summary>
    /// <param name="f">The function to convert.</param>
    /// <typeparam name="T">Return type of the function.</typeparam>
    /// <returns>An action that will execute the function, discarding its return type.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Action Action<T>(this Func<T> f) =>
        () => f();

    /// <summary>
    /// Converts a function that takes a parameter and returns a value into an Action that takes
    /// a parameter.
    /// </summary>
    /// <param name="f">The function to convert.</param>
    /// <typeparam name="T">Input type of the function and the resulting action.</typeparam>
    /// <typeparam name="TOut">Output type of the function, to be discarded.</typeparam>
    /// <returns>An action that takes a parameter and executes the function, discarding its return value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Action<T> Action<T, TOut>(this Func<T, TOut> f) =>
        t => f(t);

    /// <summary>
    /// Converts an action into a function returning unit.
    /// </summary>
    /// <param name="f">The action.</param>
    /// <typeparam name="T">Function parameter type.</typeparam>
    /// <returns>A function that takes a parameter, runs the action with it and returns unit.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T, Unit> Function<T>(this Action<T> f) =>
        t =>
        {
            f(t);
            return unit;
        };

    /// <summary>
    /// Converts an action to a function that returns unit.
    /// </summary>
    /// <param name="f">The Action.</param>
    /// <returns>A function that returns unit.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<Unit> Function(this Action f) =>
        () =>
        {
            f();
            return unit;
        };

    /// <summary>
    /// Converts an asynchronous action that takes a parameter into an asynchronous function that returns unit.
    /// </summary>
    /// <param name="f">The asynchronous action.</param>
    /// <typeparam name="T">Function parameter type.</typeparam>
    /// <returns>Unit in a task.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T, Task<Unit>> AsyncFunction<T>(this Func<T, Task> f) =>
        async t =>
        {
            await f(t);
            return unit;
        };

    /// <summary>
    /// Converts a parameterless asynchronous action into an asynchronous function that returns unit.
    /// </summary>
    /// <param name="f">The asynchronous action.</param>
    /// <returns>An asynchronous unit-returning function.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<Task<Unit>> AsyncFunction(this Func<Task> f) =>
        async () =>
        {
            await f();
            return unit;
        };

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<Task> AsyncAction(this Func<Task<Unit>> f) =>
        async () => { await f(); };

    /// <summary>
    /// Converts a asynchronous function that takes a parameter and returns unit into an asynchronous action
    /// that takes that same parameter.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <typeparam name="T">Function parameter type.</typeparam>
    /// <returns>An asynchronous action.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T, Task> AsyncAction<T>(this Func<T, Task<Unit>> f) =>
        async t => { await f(t); };
}
