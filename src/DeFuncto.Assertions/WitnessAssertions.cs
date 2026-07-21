namespace DeFuncto.Assertions;

/// <summary>
/// Assertion extensions over <see cref="Witness"/>.
/// </summary>
public static class WitnessAssertions
{
    /// <summary>
    /// Asserts that the witness was touched at least once.
    /// </summary>
    /// <param name="self">The witness to check.</param>
    public static void ShouldHaveBeenTouched(this Witness self)
    {
        if (self.TimesCalled < 1)
            throw new AssertionFailedException("Witness was not called");
    }


    /// <summary>
    /// Asserts that the witness was touched exactly the given number of times.
    /// </summary>
    /// <param name="self">The witness to check.</param>
    /// <param name="times">The expected number of touches.</param>
    public static void ShouldHaveBeenTouched(this Witness self, int times)
    {
        if (self.TimesCalled != times)
            throw new AssertionFailedException($"Witness should have been called {times} times but was {self.TimesCalled}");
    }
}
