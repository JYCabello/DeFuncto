using DeFuncto.Extensions;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tuples
{
    public class Apply
    {
        [Fact(DisplayName = "Applies to tuple")]
        public void Tuple() =>
            ("ban", "ana")
            .Apply((a, b) => $"{a}{b}")
            .Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Applies to triple")]
        public void Triple() =>
            ("ba", "na", "na")
            .Apply((a, b, c) => $"{a}{b}{c}")
            .Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Applies to quadruple")]
        public void Quadruple() =>
            ("ba", "na", "n", "a")
            .Apply((a, b, c, d) => $"{a}{b}{c}{d}")
            .Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Applies to quintuple")]
        public void Quintuple() =>
            ("ba", "n", "a", "n", "a")
            .Apply((a, b, c, d, e) => $"{a}{b}{c}{d}{e}")
            .Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Applies to sextuple")]
        public void Sextuple() =>
            ("b", "a", "n", "a", "n", "a")
            .Apply((a, b, c, d, e, f) => $"{a}{b}{c}{d}{e}{f}")
            .Run(result => Assert.Equal("banana", result));
    }
}
