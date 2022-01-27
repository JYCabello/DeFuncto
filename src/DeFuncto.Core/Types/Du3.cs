﻿using DeFuncto.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct Du3<T1, T2, T3> : IEquatable<Du3<T1, T2, T3>>
    {
        public enum DiscriminationValue
        {
            T1,
            T2,
            T3
        }

        private readonly T1? t1;
        private readonly T2? t2;
        private readonly T3? t3;
        public readonly DiscriminationValue Discriminator;

        public Du3(T1 t1)
        {
            this.t1 = t1;
            t2 = default;
            t3 = default;
            Discriminator = DiscriminationValue.T1;
        }

        public Du3(T2 t2)
        {
            this.t2 = t2;
            t1 = default;
            t3 = default;
            Discriminator = DiscriminationValue.T2;
        }

        public Du3(T3 t3)
        {
            this.t3 = t3;
            t1 = default;
            t2 = default;
            Discriminator = DiscriminationValue.T3;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Match<T>(Func<T1, T> f1, Func<T2, T> f2, Func<T3, T> f3) =>
            Discriminator switch
            {
                DiscriminationValue.T1 => f1(t1!),
                DiscriminationValue.T2 => f2(t2!),
                DiscriminationValue.T3 => f3(t3!),
                _ => throw new ArgumentException(nameof(Discriminator))
            };

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du3<T1, T2, T3> First(T1 t1) => t1;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du3<T1, T2, T3> Second(T2 t2) => t2;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du3<T1, T2, T3> Third(T3 t3) => t3;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du3<T1, T2, T3>(T1 t1) => new(t1);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du3<T1, T2, T3>(T2 t2) => new(t2);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du3<T1, T2, T3>(T3 t3) => new(t3);

        public override bool Equals(object obj) =>
            obj is Du3<T1, T2, T3> other && Equals(other);

        public bool Equals(Du3<T1, T2, T3> other) =>
             Discriminator == other.Discriminator
             && Match(
                 v => v!.Equals(other.t1),
                 v => v!.Equals(other.t2),
                 v => v!.Equals(other.t3));

        public override int GetHashCode() =>
            (this, -735972157)
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T1?>.Default.GetHashCode(t.Item1.t1)))
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T2?>.Default.GetHashCode(t.Item1.t2)))
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T3?>.Default.GetHashCode(t.Item1.t3)))
                .Apply(t => t.Item2 * -1521134295 + t.Item1.Discriminator.GetHashCode());

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T1> ont1, Action<T2> ont2, Action<T3> ont3) =>
            Match(
                t1 =>
                {
                    ont1(t1);
                    return unit;
                },
                t2 =>
                {
                    ont2(t2);
                    return unit;
                },
                t3 =>
                {
                    ont3(t3);
                    return unit;
                });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T1> fSome) =>
            Iter(fSome, _ => { }, _ => { });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T2> fSome) =>
            Iter(_ => { }, fSome, _ => { });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T3> fSome) =>
            Iter(_ => { }, _ => { }, fSome);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T1, Unit> ont1, Func<T2, Unit> ont2, Func<T3, Unit> ont3) =>
            Iter(t1 => { ont1(t1); }, t2 => { ont2(t2); }, t3 => { ont3(t3); });
    }
}
