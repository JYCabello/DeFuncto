using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du6
{
    public class Match
    {
        [Property(DisplayName = "Matches the First option")]
        public void OnFirst(NonNull<string> a, NonNull<string> b) =>
            First<string, int, int, int, int, int>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));

        [Property(DisplayName = "Matches the Second option")]
        public void OnSecond(NonNull<string> a, NonNull<string> b) =>
            Second<int, string, int, int, int, int>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));

        [Property(DisplayName = "Matches the Third option")]
        public void OnThird(NonNull<string> a, NonNull<string> b) =>
            Third<int, int, string, int, int, int>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));

        [Property(DisplayName = "Matches the Fourth option")]
        public void OnFourth(NonNull<string> a, NonNull<string> b) =>
            Fourth<int, int, int, string, int, int>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));

        [Property(DisplayName = "Matches the Fifth option")]
        public void OnFifth(NonNull<string> a, NonNull<string> b) =>
            Fifth<int, int, int, int, string, int>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));

        [Property(DisplayName = "Matches the Sixth option")]
        public void OnSixth(NonNull<string> a, NonNull<string> b) =>
            Sixth<int, int, int, int, int, string>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));
    }
}
