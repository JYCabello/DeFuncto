namespace DeFuncto.Assertions
{
    public static class WitnessAssertions
    {
        public static void ShouldHaveBeenCalled(this Witness self)
        {
            if (self.TimesCalled < 1)
                throw new AssertionFailed("Witness was not called");
        }


        public static void ShouldHaveBeenCalled(this Witness self, int times)
        {
            if (self.TimesCalled != times)
                throw new AssertionFailed($"Witness should have been called {times} times but was {self.TimesCalled}");
        }
    }
}
