using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct Du<T1, T2> : IEquatable<Du<T1, T2>>
    {
        public enum DiscriminationValue
        {
            T1,
            T2
        }

        private readonly T1? t1;
        private readonly T2? t2;
        public readonly DiscriminationValue Discriminator;

        public Du(T1 t1)
        {
            this.t1 = t1;
            t2 = default;
            Discriminator = DiscriminationValue.T1;
        }

        public Du(T2 t2)
        {
            this.t2 = t2;
            t1 = default;
            Discriminator = DiscriminationValue.T2;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Match<T>(Func<T1, T> f1, Func<T2, T> f2) =>
            Discriminator switch
            {
                DiscriminationValue.T1 => f1(t1!),
                DiscriminationValue.T2 => f2(t2!),
                _ => throw new ArgumentException(nameof(Discriminator))
            };

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du<T1, T2> First(T1 t1) => t1;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du<T1, T2> Second(T2 t2) => t2;

        public override bool Equals(object obj) =>
            obj is Du<T1, T2> other && Equals(other);

        public bool Equals(Du<T1, T2> other) =>
             Discriminator == other.Discriminator
             && Match(v => v!.Equals(other.t1), v => v!.Equals(other.t2));

        public override int GetHashCode() =>
            (this, -1956924612)
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T1?>.Default.GetHashCode(t.Item1.t1)))
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T2?>.Default.GetHashCode(t.Item1.t2)))
                .Apply(t => t.Item2 * -1521134295 + t.Item1.Discriminator.GetHashCode());

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du<T1, T2>(T1 t1) => new(t1);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du<T1, T2>(T2 t2) => new(t2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T1> iterator)
        {
           throw new NotImplementedException();
        }       
    }
}
