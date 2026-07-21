using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using DeFuncto.Extensions;
using DeFuncto.Serialization;
using static DeFuncto.Prelude;

namespace DeFuncto;

/// <summary>
/// Unbiased discriminated union of two possible values.
/// </summary>
/// <typeparam name="T1">First case type.</typeparam>
/// <typeparam name="T2">Second case type.</typeparam>
[Newtonsoft.Json.JsonConverter(typeof(DuNewtonsoftConverter))]
public readonly struct Du<T1, T2> : IEquatable<Du<T1, T2>>
{
    /// <summary>
    /// Identifies which case a Du holds.
    /// </summary>
    public enum DiscriminationValue
    {
        /// <summary>
        /// The first case.
        /// </summary>
        T1,

        /// <summary>
        /// The second case.
        /// </summary>
        T2
    }

    private readonly T1? t1;
    private readonly T2? t2;

    /// <summary>
    /// Identifies the active case.
    /// </summary>
    public readonly DiscriminationValue Discriminator;

    /// <summary>
    /// Constructor for the first case.
    /// </summary>
    /// <param name="t1">First case type.</param>
    public Du(T1 t1)
    {
        this.t1 = t1;
        t2 = default;
        Discriminator = DiscriminationValue.T1;
    }

    /// <summary>
    /// Constructor for the second case.
    /// </summary>
    /// <param name="t2">Second case type.</param>
    public Du(T2 t2)
    {
        this.t2 = t2;
        t1 = default;
        Discriminator = DiscriminationValue.T2;
    }

    /// <summary>
    /// Collapses into a single value using the adequate projection.
    /// </summary>
    /// <param name="f1">First projection.</param>
    /// <param name="f2">Second projection.</param>
    /// <typeparam name="T">Output type.</typeparam>
    /// <returns>Output of the projection.</returns>
    /// <exception cref="ArgumentException">Only possible if the default constructor is used.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Match<T>(Func<T1, T> f1, Func<T2, T> f2) =>
        Discriminator switch
        {
            DiscriminationValue.T1 => f1(t1!),
            DiscriminationValue.T2 => f2(t2!),
            _ => throw new ArgumentException(nameof(Discriminator))
        };

    /// <summary>
    /// Produces an instance of the first case.
    /// </summary>
    /// <param name="t1">First case type.</param>
    /// <returns>A discriminated union.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du<T1, T2> First(T1 t1) => t1;

    /// <summary>
    /// Produces an instance of the second case.
    /// </summary>
    /// <param name="t2">Second case type.</param>
    /// <returns>A discriminated union.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Du<T1, T2> Second(T2 t2) => t2;

    /// <summary>
    /// Implicitly wraps a value into the first case.
    /// </summary>
    /// <param name="t1">First case value.</param>
    /// <returns>A discriminated union.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Du<T1, T2>(T1 t1) => new(t1);

    /// <summary>
    /// Implicitly wraps a value into the second case.
    /// </summary>
    /// <param name="t2">Second case value.</param>
    /// <returns>A discriminated union.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Du<T1, T2>(T2 t2) => new(t2);

    /// <summary>
    /// Determines whether this instance equals another object.
    /// </summary>
    /// <param name="obj">Object to compare with.</param>
    /// <returns>True if equal.</returns>
    public override bool Equals(object obj) =>
        obj is Du<T1, T2> other && Equals(other);

    /// <summary>
    /// Determines whether this instance equals another instance.
    /// </summary>
    /// <param name="other">Instance to compare with.</param>
    /// <returns>True if both hold the same case with equal values.</returns>
    public bool Equals(Du<T1, T2> other) =>
        Discriminator == other.Discriminator
        && Match(v => v!.Equals(other.t1), v => v!.Equals(other.t2));

    /// <summary>
    /// Computes the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode() =>
        (this, -1956924612)
        .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T1?>.Default.GetHashCode(t.Item1.t1)))
        .Apply(t => (t.Item1, t.Item2 * -1521134295 + EqualityComparer<T2?>.Default.GetHashCode(t.Item1.t2)))
        .Apply(t => t.Item2 * -1521134295 + t.Item1.Discriminator.GetHashCode());

    /// <summary>
    /// Runs an effectful function for the adequate case.
    /// </summary>
    /// <param name="ont1">First effectful function.</param>
    /// <param name="ont2">Second effectful function.</param>
    /// <returns>Unit</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Action<T1> ont1, Action<T2> ont2) =>
        Iter(ont1.Function(), ont2.Function());

    /// <summary>
    /// Runs an effectful function for the adequate case.
    /// </summary>
    /// <param name="ont1">First effectful function.</param>
    /// <param name="ont2">Second effectful function.</param>
    /// <returns>Unit</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<T1, Unit> ont1, Func<T2, Unit> ont2) =>
        Match(ont1, ont2);

    /// <summary>
    /// Runs an effectful function for the adequate case.
    /// </summary>
    /// <param name="ont1">First effectful function.</param>
    /// <returns>Unit</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<T1, Unit> ont1) =>
        Iter(ont1, _ => unit);

    /// <summary>
    /// Runs an effectful function for the adequate case.
    /// </summary>
    /// <param name="ont2">Second effectful function.</param>
    /// <returns>Unit</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<T2, Unit> ont2) =>
        Iter(_ => unit, ont2);
}
