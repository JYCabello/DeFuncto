using System;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du3
{
    public class Match
    {
        [Property(DisplayName = "Matches the first option")]
        public void OnFirst(NonNull<string> a, NonNull<string> b) =>
            First<string, int, DateTime>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));

        [Property(DisplayName = "Matches the second option")]
        public void OnSecond(NonNull<string> a, NonNull<string> b) =>
            Second<int, string, DateTime>(a.Get).Match(
               val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));

        [Property(DisplayName = "Matches the third option")]
        public void OnThird(NonNull<string> a, NonNull<string> b) =>
            Third<int, DateTime, string>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));
    }
}
