using static DeFuncto.Prelude;

namespace DeFuncto.Assertions
{
    public class Witness
    {
        public int TimesCalled { get; private set; }

        public Unit Touch()
        {
            TimesCalled++;
            return unit;
        }
    }
}
