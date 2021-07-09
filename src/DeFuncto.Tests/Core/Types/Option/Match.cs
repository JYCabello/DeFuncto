using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Match
    {
        [Fact(DisplayName = "Matches on some")]
        public void OnSome() =>
            Some("ban")
                .Match(val => $"{val}ana", () => "pear")
                .Run(val => Assert.Equal("banana", val));

        [Fact(DisplayName = "Matches on none")]
        public void OnNone() =>
            None
                .Option<string>()
                .Match(val => $"{val}pear", () => "banana")
                .Run(val => Assert.Equal("banana", val));
    }
}
