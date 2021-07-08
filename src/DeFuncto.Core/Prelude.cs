using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
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
    }
}
