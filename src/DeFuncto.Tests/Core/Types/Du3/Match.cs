using System;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du3
{
    public class Match
    {
        [Fact(DisplayName = "Matches the first option")]
        public void OnFirst() =>
            First<string, int, DateTime>("ban").Match(
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana"
            ).Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Matches the second option")]
        public void OnSecond() =>
            Second<int, string, DateTime>("ban").Match(
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana"
            ).Run(result => Assert.Equal("banana", result));

        [Fact(DisplayName = "Matches the third option")]
        public void OnThird() =>
            Third<int, DateTime, string>("ban").Match(
                val => $"{val}ana",
                val => $"{val}ana",
                val => $"{val}ana"
            ).Run(result => Assert.Equal("banana", result));
    }
}
