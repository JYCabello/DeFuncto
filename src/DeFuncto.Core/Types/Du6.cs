using DeFuncto.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct Du6<T1, T2, T3, T4, T5, T6> : IEquatable<Du6<T1, T2, T3, T4, T5, T6>>
    {
        public enum DiscriminationValue
        {
            T1,
            T2,
            T3,
            T4,
            T5,
            T6
        }

        private readonly T1? t1;
        private readonly T2? t2;
        private readonly T3? t3;
        private readonly T4? t4;
        private readonly T5? t5;
        private readonly T6? t6;
        public readonly DiscriminationValue Discriminator;

        public Du6(T1 t1)
        {
            this.t1 = t1;
            t2 = default;
            t3 = default;
            t4 = default;
            t5 = default;
            t6 = default;
            Discriminator = DiscriminationValue.T1;
        }

        public Du6(T2 t2)
        {
            this.t2 = t2;
            t1 = default;
            t3 = default;
            t4 = default;
            t5 = default;
            t6 = default;
            Discriminator = DiscriminationValue.T2;
        }

        public Du6(T3 t3)
        {
            this.t3 = t3;
            t1 = default;
            t2 = default;
            t4 = default;
            t5 = default;
            t6 = default;
            Discriminator = DiscriminationValue.T3;
        }

        public Du6(T4 t4)
        {
            this.t4 = t4;
            t1 = default;
            t2 = default;
            t3 = default;
            t5 = default;
            t6 = default;
            Discriminator = DiscriminationValue.T4;
        }

        public Du6(T5 t5)
        {
            this.t5 = t5;
            t1 = default;
            t2 = default;
            t3 = default;
            t4 = default;
            t6 = default;
            Discriminator = DiscriminationValue.T5;
        }

        public Du6(T6 t6)
        {
            this.t6 = t6;
            t1 = default;
            t2 = default;
            t3 = default;
            t4 = default;
            t5 = default;
            Discriminator = DiscriminationValue.T6;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Match<T>(Func<T1, T> f1, Func<T2, T> f2, Func<T3, T> f3, Func<T4, T> f4, Func<T5, T> f5, Func<T6, T> f6) =>
            Discriminator switch
            {
                DiscriminationValue.T1 => f1(t1!),
                DiscriminationValue.T2 => f2(t2!),
                DiscriminationValue.T3 => f3(t3!),
                DiscriminationValue.T4 => f4(t4!),
                DiscriminationValue.T5 => f5(t5!),
                DiscriminationValue.T6 => f6(t6!),
                _ => throw new ArgumentException(nameof(Discriminator))
            };

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> First(T1 t1) => t1;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> Second(T2 t2) => t2;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> Third(T3 t3) => t3;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> Fourth(T4 t4) => t4;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> Fifth(T5 t5) => t5;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Du6<T1, T2, T3, T4, T5, T6> Sixth(T6 t6) => t6;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du6<T1, T2, T3, T4, T5, T6>(T1 t1) => new(t1);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du6<T1, T2, T3, T4, T5, T6>(T2 t2) => new(t2);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du6<T1, T2, T3, T4, T5, T6>(T3 t3) => new(t3);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du6<T1, T2, T3, T4, T5, T6>(T4 t4) => new(t4);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du6<T1, T2, T3, T4, T5, T6>(T5 t5) => new(t5);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Du6<T1, T2, T3, T4, T5, T6>(T6 t6) => new(t6);

        public override bool Equals(object obj) =>
            obj is Du6<T1, T2, T3, T4, T5, T6> other && Equals(other);

        public bool Equals(Du6<T1, T2, T3, T4, T5, T6> other) =>
             Discriminator == other.Discriminator
             && Match(
                 v => v!.Equals(other.t1),
                 v => v!.Equals(other.t2),
                 v => v!.Equals(other.t3),
                 v => v!.Equals(other.t4),
                 v => v!.Equals(other.t5),
                 v => v!.Equals(other.t6));

        public override int GetHashCode() =>
            (this, -315974987)
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T1?>.Default.GetHashCode(t.Item1.t1)))
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T2?>.Default.GetHashCode(t.Item1.t2)))
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T3?>.Default.GetHashCode(t.Item1.t3)))
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T4?>.Default.GetHashCode(t.Item1.t4)))
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T5?>.Default.GetHashCode(t.Item1.t5)))
                .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T6?>.Default.GetHashCode(t.Item1.t6)))
                .Apply(t => t.Item2 * -1521134295 + t.Item1.Discriminator.GetHashCode());

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T1> ont1, Action<T2> ont2, Action<T3> ont3, Action<T4> ont4, Action<T5> ont5, Action<T6> ont6) =>
            Match(
                t1 => { ont1(t1); return unit; },
                t2 => { ont2(t2); return unit; },
                t3 => { ont3(t3); return unit; },
                t4 => { ont4(t4); return unit; },
                t5 => { ont5(t5); return unit; },
                t6 => { ont6(t6); return unit; }
            );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T1> fSome) =>
            Iter(fSome, _ => { }, _ => { }, _ => { }, _ => { }, _ => { });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T2> fSome) =>
            Iter(_ => { }, fSome, _ => { }, _ => { }, _ => { }, _ => { });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T3> fSome) =>
            Iter(_ => { }, _ => { }, fSome, _ => { }, _ => { }, _ => { });
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T4> fSome) =>
            Iter(_ => { }, _ => { }, _ => { }, fSome, _ => { }, _ => { });
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T5> fSome) =>
            Iter(_ => { }, _ => { }, _ => { }, _ => { }, fSome, _ => { });
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T6> fSome) =>
            Iter(_ => { }, _ => { }, _ => { }, _ => { }, _ => { }, fSome);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T1, Unit> ont1, Func<T2, Unit> ont2, Func<T3, Unit> ont3, Func<T4, Unit> ont4, Func<T5, Unit> ont5, Func<T6, Unit> ont6) =>
            Iter(
                t1 => { ont1(t1); },
                t2 => { ont2(t2); },
                t3 => { ont3(t3); },
                t4 => { ont4(t4); },
                t5 => { ont5(t5); },
                t6 => { ont6(t6); }
            );

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T1, Unit> ont1) =>
            Iter(t1 => { ont1(t1); });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T2, Unit> ont2) =>
            Iter(t2 => { ont2(t2); });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T3, Unit> ont3) =>
            Iter(t3 => { ont3(t3); });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T4, Unit> ont4) =>
            Iter(t4 => { ont4(t4); });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T5, Unit> ont2) =>
            Iter(t5 => { ont2(t5); });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T6, Unit> ont3) =>
            Iter(t6 => { ont3(t6); });
    }
}
