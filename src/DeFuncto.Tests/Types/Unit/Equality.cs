using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.Unit
{
    public class Equality
    {
        [Fact(DisplayName = "All units are equal")]
        public void EqualityOverriden()
        {
            Assert.Equal(unit, new DeFuncto.Unit());
            Assert.Equal(unit, DeFuncto.Unit.Default);
            Assert.Equal(new DeFuncto.Unit(), DeFuncto.Unit.Default);
            Assert.Equal(new DeFuncto.Unit(), new DeFuncto.Unit());
            Assert.True(unit.Equals(new DeFuncto.Unit()));
            Assert.Equal(new DeFuncto.Unit().GetHashCode(), new DeFuncto.Unit().GetHashCode());
        }
    }
}
