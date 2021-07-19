using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du7
{
    public class Match
    {
        [Property(DisplayName = "Matches the First option")]
        public void OnFirst(
            NonNull<string> a,
            NonNull<string> b,
            int c,
            int d,
            int e,
            int f,
            int g,
            int h
        ) =>
            First<string, int, int, int, int, int, int>(b.Get).Match(
                val => $"{val}{a.Get}",
                _ => $"{c}{a.Get}",
                _ => $"{d}{a.Get}",
                _ => $"{e}{a.Get}",
                _ => $"{f}{a.Get}",
                _ => $"{g}{a.Get}",
                _ => $"{g}{a.Get}"
            ).Run(result => Assert.Equal($"{b.Get}{a.Get}", result));

        [Property(DisplayName = "Matches the Second option")]
        public void OnSecond(
            NonNull<string> a,
            int b,
            NonNull<string> c,
            int d,
            int e,
            int f,
            int g,
            int h
        ) =>
            Second<int, string, int, int, int, int, int>(c.Get).Match(
                _ => $"{b}{a.Get}",
                val => $"{val}{a.Get}",
                _ => $"{d}{a.Get}",
                _ => $"{e}{a.Get}",
                _ => $"{f}{a.Get}",
                _ => $"{g}{a.Get}",
                _ => $"{g}{a.Get}"
            ).Run(result => Assert.Equal($"{c.Get}{a.Get}", result));

        [Property(DisplayName = "Matches the Third option")]
        public void OnThird(
            NonNull<string> a,
            int b,
            int c,
            NonNull<string> d,
            int e,
            int f,
            int g,
            int h
        ) =>
            Third<int, int, string, int, int, int, int>(d.Get).Match(
                _ => $"{b}{a.Get}",
                _ => $"{c}{a.Get}",
                val => $"{val}{a.Get}",
                _ => $"{e}{a.Get}",
                _ => $"{f}{a.Get}",
                _ => $"{g}{a.Get}",
                _ => $"{g}{a.Get}"
            ).Run(result => Assert.Equal($"{d.Get}{a.Get}", result));

        [Property(DisplayName = "Matches the Fourth option")]
        public void OnFourth(NonNull<string> a, NonNull<string> b) =>
            Fourth<int, int, int, string, int, int, int>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));

        [Property(DisplayName = "Matches the Fifth option")]
        public void OnFifth(NonNull<string> a, NonNull<string> b) =>
            Fifth<int, int, int, int, string, int, int>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));

        [Property(DisplayName = "Matches the Sixth option")]
        public void OnSixth(NonNull<string> a, NonNull<string> b) =>
            Sixth<int, int, int, int, int, string, int>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));

        [Property(DisplayName = "Matches the Seventh option")]
        public void OnSeventh(NonNull<string> a, NonNull<string> b) =>
            Seventh<int, int, int, int, int, int, string>(a.Get).Match(
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}",
                val => $"{val}{b.Get}"
            ).Run(result => Assert.Equal(a.Get + b.Get, result));
    }
}
