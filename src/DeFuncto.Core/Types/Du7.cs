﻿using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace DeFuncto
{
    public readonly struct Du7<T1, T2, T3, T4, T5, T6, T7>
    {
        public enum DiscriminationValue
        {
            T1,
            T2,
            T3,
            T4,
            T5,
            T6,
            T7
        }

        private readonly T1? t1;
        private readonly T2? t2;
        private readonly T3? t3;
        private readonly T4? t4;
        private readonly T5? t5;
        private readonly T6? t6;
        private readonly T7? t7;
        public readonly DiscriminationValue Discriminator;

        public Du7(T1 t1)
        {
            this.t1 = t1;
            t2 = default;
            t3 = default;
            t4 = default;
            t5 = default;
            t6 = default;
            t7 = default;
            Discriminator = DiscriminationValue.T1;
        }

        public Du7(T2 t2)
        {
            this.t2 = t2;
            t1 = default;
            t3 = default;
            t4 = default;
            t5 = default;
            t6 = default;
            t7 = default;
            Discriminator = DiscriminationValue.T2;
        }

        public Du7(T3 t3)
        {
            this.t3 = t3;
            t1 = default;
            t2 = default;
            t4 = default;
            t5 = default;
            t6 = default;
            t7 = default;
            Discriminator = DiscriminationValue.T3;
        }

        public Du7(T4 t4)
        {
            this.t4 = t4;
            t1 = default;
            t2 = default;
            t3 = default;
            t5 = default;
            t6 = default;
            t7 = default;
            Discriminator = DiscriminationValue.T4;
        }

        public Du7(T5 t5)
        {
            this.t5 = t5;
            t1 = default;
            t2 = default;
            t3 = default;
            t4 = default;
            t6 = default;
            t7 = default;
            Discriminator = DiscriminationValue.T5;
        }

        public Du7(T6 t6)
        {
            this.t6 = t6;
            t1 = default;
            t2 = default;
            t3 = default;
            t4 = default;
            t5 = default;
            t7 = default;
            Discriminator = DiscriminationValue.T6;
        }

        public Du7(T7 t7)
        {
            this.t7 = t7;
            t1 = default;
            t2 = default;
            t3 = default;
            t4 = default;
            t5 = default;
            t6 = default;
            Discriminator = DiscriminationValue.T7;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Match<T>(Func<T1, T> f1, Func<T2, T> f2, Func<T3, T> f3, Func<T4, T> f4, Func<T5, T> f5, Func<T6, T> f6, Func<T7, T> f7) =>
            Discriminator switch
            {
                DiscriminationValue.T1 => f1(t1!),
                DiscriminationValue.T2 => f2(t2!),
                DiscriminationValue.T3 => f3(t3!),
                DiscriminationValue.T4 => f4(t4!),
                DiscriminationValue.T5 => f5(t5!),
                DiscriminationValue.T6 => f6(t6!),
                DiscriminationValue.T7 => f7(t7!),
                _ => throw new ArgumentException(nameof(Discriminator))
            };

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> First(T1 t1) => t1;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Second(T2 t2) => t2;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Third(T3 t3) => t3;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Fourth(T4 t4) => t4;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Fifth(T5 t5) => t5;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Sixth(T6 t6) => t6;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du7<T1, T2, T3, T4, T5, T6, T7> Seventh(T7 t7) => t7;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du7<T1, T2, T3, T4, T5, T6, T7>(T1 t1) => new(t1);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du7<T1, T2, T3, T4, T5, T6, T7>(T2 t2) => new(t2);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du7<T1, T2, T3, T4, T5, T6, T7>(T3 t3) => new(t3);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du7<T1, T2, T3, T4, T5, T6, T7>(T4 t4) => new(t4);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du7<T1, T2, T3, T4, T5, T6, T7>(T5 t5) => new(t5);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du7<T1, T2, T3, T4, T5, T6, T7>(T6 t6) => new(t6);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du7<T1, T2, T3, T4, T5, T6, T7>(T7 t7) => new(t7);
    }
}
