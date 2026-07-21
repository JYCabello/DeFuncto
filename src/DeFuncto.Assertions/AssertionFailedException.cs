using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DeFuncto.Assertions;

/// <summary>
/// Exception thrown when a DeFuncto assertion fails.
/// </summary>
[ExcludeFromCodeCoverage]
[Serializable]
public sealed class AssertionFailedException : Exception
{
    private AssertionFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    /// <summary>
    /// Creates a new assertion failure exception.
    /// </summary>
    /// <param name="message">The failure message.</param>
    public AssertionFailedException(string message)
        : base($"Assertion failed: {message}") { }
}
