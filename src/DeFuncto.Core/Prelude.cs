using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.ActivePatternMatching;
using DeFuncto.Extensions;

namespace DeFuncto;

/// <summary>
/// Basic functional operations.
/// </summary>
public static class Prelude
{
    /// <summary>
    /// Option in the None state.
    /// </summary>
    public static OptionNone None => new();

    /// <summary>
    /// Default access for the unit instance.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static Unit unit => Unit.Default;

    /// <summary>
    /// Creates a ResultOk, will usually convert to Result via implicit casting.
    /// </summary>
    /// <param name="ok">Value.</param>
    /// <typeparam name="TOk">Value type.</typeparam>
    /// <returns>ResultOk</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ResultOk<TOk> Ok<TOk>(TOk ok) => new(ok);

    /// <summary>
    /// Creates a Result in the Ok state.
    /// </summary>
    /// <param name="ok">Value.</param>
    /// <typeparam name="TOk">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <returns>A Result in the Ok state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOk, TError> Ok<TOk, TError>(TOk ok) => Ok(ok);

    /// <summary>
    /// Creates a ResultError, will usually convert to Result via implicit casting.
    /// </summary>
    /// <param name="error">Error value.</param>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <returns>ResultError</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ResultError<TError> Error<TError>(TError error) => new(error);

    /// <summary>
    /// Creates a Result in the Error state.
    /// </summary>
    /// <param name="error">Error value.</param>
    /// <typeparam name="TOk">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <returns>A Result in the Error state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOk, TError> Error<TOk, TError>(TError error) => Error(error);

    /// <summary>
    /// Identity function, returns the value passed to it.
    /// </summary>
    /// <param name="t">Value.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>The value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Id<T>(T t) => t;

    /// <summary>
    /// Creates an Option in the Some state.
    /// </summary>
    /// <param name="t"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Some<T>(T t) => Option<T>.Some(t);

    /// <summary>
    /// Creates an Option out of a nullable, None for null, Some for non null.
    /// </summary>
    /// <param name="t">Nullable value.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>An option in the corresponding state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Optional<T>(T? t) =>
        t is not null ? Some(t) : None;

    /// <summary>
    /// Creates an Option out of a nullable, None for null, Some for non null.
    /// </summary>
    /// <param name="t">Nullable value.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>An option in the corresponding state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Optional<T>(T? t) where T : struct =>
        t.HasValue ? Some(t.Value) : None;

    /// <summary>
    /// Composes two functions, passing the output of the first to the second.
    /// </summary>
    /// <param name="f1">First function to compose.</param>
    /// <param name="f2">Second function to compose.</param>
    /// <typeparam name="T1">Input type of the first function.</typeparam>
    /// <typeparam name="T2">Output type of the first function and input of the second.</typeparam>
    /// <typeparam name="T3">Output type of the second function.</typeparam>
    /// <returns>A function than takes the input, </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> f1, Func<T2, T3> f2) =>
        t1 => t1.Apply(f1).Apply(f2);

    /// <summary>
    /// Composes two functions, passing the output of the first to the second.
    /// </summary>
    /// <param name="f1">First function to compose.</param>
    /// <param name="f2">Second function to compose.</param>
    /// <typeparam name="T1">Input type of the first function.</typeparam>
    /// <typeparam name="T2">Output type of the first function and input of the second.</typeparam>
    /// <typeparam name="T3">Output type of the second function.</typeparam>
    /// <returns>A function than takes the input, </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T2> Compose<T1, T2>(this Func<T1> f1, Func<T1, T2> f2) =>
        () => f1().Apply(f2);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du<T1, T2> First<T1, T2>(T1 t1) => Du<T1, T2>.First(t1);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du3<T1, T2, T3> First<T1, T2, T3>(T1 t1) => Du3<T1, T2, T3>.First(t1);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du4<T1, T2, T3, T4> First<T1, T2, T3, T4>(T1 t1) => Du4<T1, T2, T3, T4>.First(t1);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du5<T1, T2, T3, T4, T5> First<T1, T2, T3, T4, T5>(T1 t1) => Du5<T1, T2, T3, T4, T5>.First(t1);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du6<T1, T2, T3, T4, T5, T6> First<T1, T2, T3, T4, T5, T6>(T1 t1) => Du6<T1, T2, T3, T4, T5, T6>.First(t1);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du7<T1, T2, T3, T4, T5, T6, T7> First<T1, T2, T3, T4, T5, T6, T7>(T1 t1) => Du7<T1, T2, T3, T4, T5, T6, T7>.First(t1);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du<T1, T2> Second<T1, T2>(T2 t2) => Du<T1, T2>.Second(t2);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du3<T1, T2, T3> Second<T1, T2, T3>(T2 t2) => Du3<T1, T2, T3>.Second(t2);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du4<T1, T2, T3, T4> Second<T1, T2, T3, T4>(T2 t2) => Du4<T1, T2, T3, T4>.Second(t2);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du5<T1, T2, T3, T4, T5> Second<T1, T2, T3, T4, T5>(T2 t2) => Du5<T1, T2, T3, T4, T5>.Second(t2);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du6<T1, T2, T3, T4, T5, T6> Second<T1, T2, T3, T4, T5, T6>(T2 t2) => Du6<T1, T2, T3, T4, T5, T6>.Second(t2);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du7<T1, T2, T3, T4, T5, T6, T7> Second<T1, T2, T3, T4, T5, T6, T7>(T2 t2) => Du7<T1, T2, T3, T4, T5, T6, T7>.Second(t2);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du3<T1, T2, T3> Third<T1, T2, T3>(T3 t3) => Du3<T1, T2, T3>.Third(t3);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du4<T1, T2, T3, T4> Third<T1, T2, T3, T4>(T3 t3) => Du4<T1, T2, T3, T4>.Third(t3);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du5<T1, T2, T3, T4, T5> Third<T1, T2, T3, T4, T5>(T3 t3) => Du5<T1, T2, T3, T4, T5>.Third(t3);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du6<T1, T2, T3, T4, T5, T6> Third<T1, T2, T3, T4, T5, T6>(T3 t3) => Du6<T1, T2, T3, T4, T5, T6>.Third(t3);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du7<T1, T2, T3, T4, T5, T6, T7> Third<T1, T2, T3, T4, T5, T6, T7>(T3 t3) => Du7<T1, T2, T3, T4, T5, T6, T7>.Third(t3);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du4<T1, T2, T3, T4> Fourth<T1, T2, T3, T4>(T4 t4) => Du4<T1, T2, T3, T4>.Fourth(t4);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du5<T1, T2, T3, T4, T5> Fourth<T1, T2, T3, T4, T5>(T4 t4) => Du5<T1, T2, T3, T4, T5>.Fourth(t4);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du6<T1, T2, T3, T4, T5, T6> Fourth<T1, T2, T3, T4, T5, T6>(T4 t4) => Du6<T1, T2, T3, T4, T5, T6>.Fourth(t4);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du7<T1, T2, T3, T4, T5, T6, T7> Fourth<T1, T2, T3, T4, T5, T6, T7>(T4 t4) => Du7<T1, T2, T3, T4, T5, T6, T7>.Fourth(t4);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du5<T1, T2, T3, T4, T5> Fifth<T1, T2, T3, T4, T5>(T5 t5) => Du5<T1, T2, T3, T4, T5>.Fifth(t5);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du6<T1, T2, T3, T4, T5, T6> Fifth<T1, T2, T3, T4, T5, T6>(T5 t5) => Du6<T1, T2, T3, T4, T5, T6>.Fifth(t5);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du7<T1, T2, T3, T4, T5, T6, T7> Fifth<T1, T2, T3, T4, T5, T6, T7>(T5 t5) => Du7<T1, T2, T3, T4, T5, T6, T7>.Fifth(t5);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du6<T1, T2, T3, T4, T5, T6> Sixth<T1, T2, T3, T4, T5, T6>(T6 t6) => Du6<T1, T2, T3, T4, T5, T6>.Sixth(t6);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du7<T1, T2, T3, T4, T5, T6, T7> Sixth<T1, T2, T3, T4, T5, T6, T7>(T6 t6) => Du7<T1, T2, T3, T4, T5, T6, T7>.Sixth(t6);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du7<T1, T2, T3, T4, T5, T6, T7> Seventh<T1, T2, T3, T4, T5, T6, T7>(T7 t7) => Du7<T1, T2, T3, T4, T5, T6, T7>.Seventh(t7);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut ActMatch<TIn, TOut>(
        this TIn input,
        ActivePatternBase<TIn, TOut>[] patterns,
        Func<TIn, TOut> defaulter
    ) =>
        ActivePatternsExecutor.Execute(input, patterns, defaulter);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ActivePatternBase<TIn, TOut> Pattern<TIn, TIntermediate, TOut>(
        Func<TIn, Option<TIntermediate>> matcher,
        Func<TIntermediate, TOut> projection
    ) =>
        new ActivePattern<TIn, TIntermediate, TOut>(matcher, projection);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ActivePatternBase<TIn, TOut>[] With<TIn, TOut>(params ActivePatternBase<TIn, TOut>[] patterns) =>
        patterns;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T, Exception> Try<T>(Func<T> func)
    {
        try
        {
            return func().Apply(Result<T, Exception>.Ok);
        }
        catch (Exception ex)
        {
            return Result<T, Exception>.Error(ex);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncResult<T, Exception> Try<T>(Func<Task<T>> func)
    {
        return Go();

        async Task<Result<T, Exception>> Go()
        {
            try
            {
                return await func().Map(Result<T, Exception>.Ok);
            }
            catch (Exception ex)
            {
                return Result<T, Exception>.Error(ex);
            }
        }
    }
}
