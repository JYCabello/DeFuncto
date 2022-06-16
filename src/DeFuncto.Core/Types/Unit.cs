namespace DeFuncto;

/// <summary>
/// Type representing a signal of termination of an effectful function.
/// All instances of unit are expected to act as a singleton, being all equal.
/// </summary>
public readonly struct Unit
{
    /// <summary>
    /// Default instance.
    /// </summary>
    public static Unit Default { get; } = new();
    public override bool Equals(object obj) => obj is Unit;
    public bool Equals(Unit _) => true;
    public override int GetHashCode() => 0;
}
