using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;


namespace DeFuncto.Optics;

/// <summary>
/// Interface to get and set values in an object.
/// </summary>
/// <typeparam name="A">Object type.</typeparam>
/// <typeparam name="B">Object type.</typeparam>
public readonly struct Lens<A, B>
{
    public Lens(Func<A, B> get, Func<B, A, A> set)
    {
        GetF = get;
        SetF = set;
    }

    private Func<A, B> GetF { get; }
    private Func<B, A, A> SetF { get; }
    
    /// <summary>
    /// Get property of type B in an object of type A
    /// </summary>
    /// <returns>Value of property of type B.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public B Get<A, B>(Lens<A, B> lens, A item) => lens.GetF(item);
    
    /// <summary>
    /// Set value of a property of type B in an object of type A.
    /// </summary>
    /// <returns>Type A.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public A Set<A, B>(Lens<A, B> lens, A item, B value) => lens.SetF(value, item);
    
}
