using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du7
{
    public class Match
    {
        [Fact(DisplayName = "Matches the First option")]
        public void OnFirst() =>
            First<string, int, int, int, int, int, int>("ban").Match(
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana"
            ).Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Matches the Second option")]
        public void OnSecond() =>
            Second<int, string, int, int, int, int, int>("ban").Match(
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana"
            ).Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Matches the Third option")]
        public void OnThird() =>
            Third<int, int, string, int, int, int, int>("ban").Match(
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana"
            ).Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Matches the Fourth option")]
        public void OnFourth() =>
            Fourth<int, int, int, string, int, int, int>("ban").Match(
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana"
            ).Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Matches the Fifth option")]
        public void OnFifth() =>
            Fifth<int, int, int, int, string, int, int>("ban").Match(
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana"
            ).Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Matches the Sixth option")]
        public void OnSixth() =>
            Sixth<int, int, int, int, int, string, int>("ban").Match(
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana"
            ).Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Matches the Seventh option")]
        public void OnSeventh() =>
            Seventh<int, int, int, int, int, int, string>("ban").Match(
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana"
            ).Run(result => Assert.Equal("banana", result));
    }
}
