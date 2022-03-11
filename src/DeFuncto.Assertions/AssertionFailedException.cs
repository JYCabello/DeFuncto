using System;
using System.Diagnostics.CodeAnalysis;

namespace DeFuncto.Assertions;

[ExcludeFromCodeCoverage]
[Serializable]
public class AssertionFailedException : Exception
{
    public AssertionFailedException() { }

    public AssertionFailedException(string message)
        : base($"Assertion failed: {message}") { }
}
