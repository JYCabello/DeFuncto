using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct Du7<T1, T2, T3, T4, T5, T6, T7> : IEquatable<Du7<T1, T2, T3, T4, T5, T6, T7>>
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

        public override bool Equals(object obj) =>
            obj is Du7<T1, T2, T3, T4, T5, T6, T7> other && Equals(other);

        public bool Equals(Du7<T1, T2, T3, T4, T5, T6, T7> other) =>
            Discriminator == other.Discriminator
            && Match(
                v => v!.Equals(other.t1),
                v => v!.Equals(other.t2),
                v => v!.Equals(other.t3),
                v => v!.Equals(other.t4),
                v => v!.Equals(other.t5),
                v => v!.Equals(other.t6),
                v => v!.Equals(other.t7));

        public override int GetHashCode() =>
            (this, -305974134)
            .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T1?>.Default.GetHashCode(t.Item1.t1)))
            .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T2?>.Default.GetHashCode(t.Item1.t2)))
            .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T3?>.Default.GetHashCode(t.Item1.t3)))
            .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T4?>.Default.GetHashCode(t.Item1.t4)))
            .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T5?>.Default.GetHashCode(t.Item1.t5)))
            .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T6?>.Default.GetHashCode(t.Item1.t6)))
            .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T7?>.Default.GetHashCode(t.Item1.t7)))
            .Apply(t => t.Item2 * -1521134295 + t.Item1.Discriminator.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T1> ont1, Action<T2> ont2, Action<T3> ont3, Action<T4> ont4, Action<T5> ont5, Action<T6> ont6, Action<T7> ont7) =>
            Iter(ont1.Function(), ont2.Function(), ont3.Function(), ont4.Function(), ont5.Function(), ont6.Function(), ont7.Function());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T1, Unit> ont1, Func<T2, Unit> ont2, Func<T3, Unit> ont3, Func<T4, Unit> ont4, Func<T5, Unit> ont5, Func<T6, Unit> ont6, Func<T7, Unit> ont7) =>
            Match(ont1, ont2, ont3, ont4, ont5, ont6, ont7);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T1, Unit> ont1) =>
            Iter(ont1, _ => unit, _ => unit, _ => unit, _ => unit, _ => unit, _ => unit);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T2, Unit> ont2) =>
            Iter(_ => unit, ont2, _ => unit, _ => unit, _ => unit, _ => unit, _ => unit);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T3, Unit> ont3) =>
            Iter(_ => unit, _ => unit, ont3, _ => unit, _ => unit, _ => unit, _ => unit);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T4, Unit> ont4) =>
            Iter(_ => unit, _ => unit, _ => unit, ont4, _ => unit, _ => unit, _ => unit);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T5, Unit> ont5) =>
            Iter(_ => unit, _ => unit, _ => unit, _ => unit, ont5, _ => unit, _ => unit);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T6, Unit> ont6) =>
            Iter(_ => unit, _ => unit, _ => unit, _ => unit, _ => unit, ont6, _ => unit);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T7, Unit> ont7) =>
            Iter(_ => unit, _ => unit, _ => unit, _ => unit, _ => unit, _ => unit, ont7);
    }
}
