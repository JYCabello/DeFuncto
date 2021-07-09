using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using DeFuncto.Extensions;

namespace DeFuncto
{
    public static class Prelude
    {
        public static OptionNone None => new();

        // ReSharper disable once InconsistentNaming
        public static Unit unit => Unit.Default;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ResultOk<TOk> Ok<TOk>(TOk ok) => new(ok);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TOk, TError> Ok<TOk, TError>(TOk ok) => Ok(ok);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ResultError<TError> Error<TError>(TError error) => new(error);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TOk, TError> Error<TOk, TError>(TError error) => Error(error);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Id<T>(T t) => t;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> Some<T>(T t) => Option<T>.Some(t);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> Optional<T>(T? t) =>
            t is not null ? Some(t) : None;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> f1, Func<T2, T3> f2) =>
            t1 => f2(f1(t1));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T2> Compose<T1, T2>(this Func<T1> f1, Func<T1, T2> f2) =>
            () => f1().Apply(f2);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du<T1, T2> First<T1, T2>(T1 t1) => Du<T1, T2>.First(t1);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du<T1, T2> Second<T1, T2>(T2 t2) => Du<T1, T2>.Second(t2);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du3<T1, T2, T3> First<T1, T2, T3>(T1 t1) => Du3<T1, T2, T3>.First(t1);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du3<T1, T2, T3> Second<T1, T2, T3>(T2 t2) => Du3<T1, T2, T3>.Second(t2);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du3<T1, T2, T3> Third<T1, T2, T3>(T3 t3) => Du3<T1, T2, T3>.Third(t3);
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du5<T1, T2, T3, T4, T5> First<T1, T2, T3, T4, T5>(T1 t1) => Du5<T1, T2, T3, T4, T5>.First(t1);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du5<T1, T2, T3, T4, T5> Second<T1, T2, T3, T4, T5>(T2 t2) => Du5<T1, T2, T3, T4, T5>.Second(t2);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du5<T1, T2, T3, T4, T5> Third<T1, T2, T3, T4, T5>(T3 t3) => Du5<T1, T2, T3, T4, T5>.Third(t3);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du5<T1, T2, T3, T4, T5> Fourth<T1, T2, T3, T4, T5>(T4 t4) => Du5<T1, T2, T3, T4, T5>.Fourth(t4);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du5<T1, T2, T3, T4, T5> Fifth<T1, T2, T3, T4, T5>(T5 t5) => Du5<T1, T2, T3, T4, T5>.Fifth(t5);
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> First<T1, T2, T3, T4, T5, T6>(T1 t1) => Du6<T1, T2, T3, T4, T5, T6>.First(t1);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> Second<T1, T2, T3, T4, T5, T6>(T2 t2) => Du6<T1, T2, T3, T4, T5, T6>.Second(t2);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> Third<T1, T2, T3, T4, T5, T6>(T3 t3) => Du6<T1, T2, T3, T4, T5, T6>.Third(t3);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> Fourth<T1, T2, T3, T4, T5, T6>(T4 t4) => Du6<T1, T2, T3, T4, T5, T6>.Fourth(t4);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> Fifth<T1, T2, T3, T4, T5, T6>(T5 t5) => Du6<T1, T2, T3, T4, T5, T6>.Fifth(t5);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> Sixth<T1, T2, T3, T4, T5, T6>(T6 t6) => Du6<T1, T2, T3, T4, T5, T6>.Sixth(t6);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> First<T1, T2, T3, T4, T5, T6, T7>(T1 t1) => Du7<T1, T2, T3, T4, T5, T6, T7>.First(t1);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Second<T1, T2, T3, T4, T5, T6, T7>(T2 t2) => Du7<T1, T2, T3, T4, T5, T6, T7>.Second(t2);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Third<T1, T2, T3, T4, T5, T6, T7>(T3 t3) => Du7<T1, T2, T3, T4, T5, T6, T7>.Third(t3);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Fourth<T1, T2, T3, T4, T5, T6, T7>(T4 t4) => Du7<T1, T2, T3, T4, T5, T6, T7>.Fourth(t4);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Fifth<T1, T2, T3, T4, T5, T6, T7>(T5 t5) => Du7<T1, T2, T3, T4, T5, T6, T7>.Fifth(t5);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Sixth<T1, T2, T3, T4, T5, T6, T7>(T6 t6) => Du7<T1, T2, T3, T4, T5, T6, T7>.Sixth(t6);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Seventh<T1, T2, T3, T4, T5, T6, T7>(T7 t7) => Du7<T1, T2, T3, T4, T5, T6, T7>.Seventh(t7);
    }
}
