namespace DeFuncto.Assertions;

public static class WitnessAssertions
{
    public static void ShouldHaveBeenTouched(this Witness self)
    {
        if (self.TimesCalled < 1)
            throw new AssertionFailedException("Witness was not called");
    }


    public static void ShouldHaveBeenTouched(this Witness self, int times)
    {
        if (self.TimesCalled != times)
            throw new AssertionFailedException($"Witness should have been called {times} times but was {self.TimesCalled}");
    }
}
