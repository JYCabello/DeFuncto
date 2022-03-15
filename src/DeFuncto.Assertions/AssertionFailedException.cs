using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DeFuncto.Assertions;

[ExcludeFromCodeCoverage]
[Serializable]
public sealed class AssertionFailedException : Exception
{
    private AssertionFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public AssertionFailedException(string message)
        : base($"Assertion failed: {message}") { }
}
