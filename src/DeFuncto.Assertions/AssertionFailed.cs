using System;
using System.Diagnostics.CodeAnalysis;

namespace DeFuncto.Assertions
{
    [ExcludeFromCodeCoverage]
    public class AssertionFailed : Exception
    {
        public AssertionFailed(string message)
            : base($"Assertion failed: {message}") { }
    }
}
