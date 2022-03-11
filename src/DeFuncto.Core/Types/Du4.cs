using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto;

public readonly struct Du4<T1, T2, T3, T4> : IEquatable<Du4<T1, T2, T3, T4>>
{
    public enum DiscriminationValue
    {
        T1,
        T2,
        T3,
        T4
    }

    private readonly T1? t1;
    private readonly T2? t2;
    private readonly T3? t3;
    private readonly T4? t4;
    public readonly DiscriminationValue Discriminator;

    public Du4(T1 t1)
    {
        this.t1 = t1;
        t2 = default;
        t3 = default;
        t4 = default;
        Discriminator = DiscriminationValue.T1;
    }

    public Du4(T2 t2)
    {
        this.t2 = t2;
        t1 = default;
        t3 = default;
        t4 = default;
        Discriminator = DiscriminationValue.T2;
    }

    public Du4(T3 t3)
    {
        this.t3 = t3;
        t1 = default;
        t2 = default;
        t4 = default;
        Discriminator = DiscriminationValue.T3;
    }

    public Du4(T4 t4)
    {
        this.t4 = t4;
        t1 = default;
        t2 = default;
        t3 = default;
        Discriminator = DiscriminationValue.T4;
    }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Match<T>(Func<T1, T> f1, Func<T2, T> f2, Func<T3, T> f3, Func<T4, T> f4) =>
        Discriminator switch
        {
            DiscriminationValue.T1 => f1(t1!),
            DiscriminationValue.T2 => f2(t2!),
            DiscriminationValue.T3 => f3(t3!),
            DiscriminationValue.T4 => f4(t4!),
            _ => throw new ArgumentException(nameof(Discriminator))
        };

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du4<T1, T2, T3, T4> First(T1 t1) => t1;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du4<T1, T2, T3, T4> Second(T2 t2) => t2;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du4<T1, T2, T3, T4> Third(T3 t3) => t3;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du4<T1, T2, T3, T4> Fourth(T4 t4) => t4;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Du4<T1, T2, T3, T4>(T1 t1) => new(t1);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Du4<T1, T2, T3, T4>(T2 t2) => new(t2);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Du4<T1, T2, T3, T4>(T3 t3) => new(t3);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Du4<T1, T2, T3, T4>(T4 t4) => new(t4);

    public override bool Equals(object obj) =>
        obj is Du4<T1, T2, T3, T4> other && Equals(other);

    public bool Equals(Du4<T1, T2, T3, T4> other) =>
        Discriminator == other.Discriminator
        && Match(
            v => v!.Equals(other.t1),
            v => v!.Equals(other.t2),
            v => v!.Equals(other.t3),
            v => v!.Equals(other.t4));

    public override int GetHashCode() =>
        (this, -307543185)
        .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T1?>.Default.GetHashCode(t.Item1.t1)))
        .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T2?>.Default.GetHashCode(t.Item1.t2)))
        .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T3?>.Default.GetHashCode(t.Item1.t3)))
        .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T4?>.Default.GetHashCode(t.Item1.t4)))
        .Apply(t => t.Item2 * -1521134295 + t.Item1.Discriminator.GetHashCode());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Action<T1> ont1, Action<T2> ont2, Action<T3> ont3, Action<T4> ont4) =>
        Iter(ont1.Function(), ont2.Function(), ont3.Function(), ont4.Function());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<T1, Unit> ont1, Func<T2, Unit> ont2, Func<T3, Unit> ont3, Func<T4, Unit> ont4) =>
        Match(ont1, ont2, ont3, ont4);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<T1, Unit> ont1) =>
        Iter(ont1, _ => unit, _ => unit, _ => unit);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<T2, Unit> ont2) =>
        Iter(_ => unit, ont2, _ => unit, _ => unit);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<T3, Unit> ont3) =>
        Iter(_ => unit, _ => unit, ont3, _ => unit);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<T4, Unit> ont4) =>
        Iter(_ => unit, _ => unit, _ => unit, ont4);
}
