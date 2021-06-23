using System;

namespace DeFuncto.Assertions
{
    public class AssertionFailedException : Exception
    {
        public AssertionFailedException(string message)
            : base($"Assertion failed: {message}") { }
    }
}
