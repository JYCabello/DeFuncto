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

    /// <summary>
    /// Determines whether this instance equals another object.
    /// </summary>
    /// <param name="obj">Object to compare with.</param>
    /// <returns>True if the object is a Unit.</returns>
    public override bool Equals(object obj) => obj is Unit;

    /// <summary>
    /// Determines whether this instance equals another Unit.
    /// </summary>
    /// <param name="_">Instance to compare with.</param>
    /// <returns>True, as all Unit instances are equal.</returns>
    public bool Equals(Unit _) => true;

    /// <summary>
    /// Computes the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode() => 0;
}
