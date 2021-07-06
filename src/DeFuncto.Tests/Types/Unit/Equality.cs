using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types
{
    public class Equality
    {
        private static readonly Func<Unit>[] UnitGetters =
        {
            () => unit,
            () => new Unit(),
            () => Unit.Default
        };

        public static IEnumerable<object[]> Units() =>
            from u1 in UnitGetters
            from u2 in UnitGetters
            select new object[] { u1(), u2() };

        [Theory(DisplayName = "All units are equal")]
        [MemberData(nameof(Units))]
        public void EqualityOverriden(Unit a, Unit b)
        {
            Assert.Equal(a, b);
            Assert.True(a.Equals(b));
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }
    }
}
