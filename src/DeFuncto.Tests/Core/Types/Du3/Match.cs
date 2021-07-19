using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du3
{
    public class Match
    {
        [Property(DisplayName = "Matches the First option")]
        public void OnFirst(
            NonNull<string> a,
            NonNull<string> b,
            int c,
            int d
        ) =>
            First<string, int, int>(b.Get).Match(
                val => $"{val}{a.Get}",
                _ => $"{c}{a.Get}",
                _ => $"{d}{a.Get}"
            ).Run(result => Assert.Equal($"{b.Get}{a.Get}", result));

        [Property(DisplayName = "Matches the Second option")]
        public void OnSecond(
            NonNull<string> a,
            int b,
            NonNull<string> c,
            int d
        ) =>
            Second<int, string, int>(c.Get).Match(
                _ => $"{b}{a.Get}",
                val => $"{val}{a.Get}",
                _ => $"{d}{a.Get}"
            ).Run(result => Assert.Equal($"{c.Get}{a.Get}", result));

        [Property(DisplayName = "Matches the Third option")]
        public void OnThird(
            NonNull<string> a,
            int b,
            int c,
            NonNull<string> d
        ) =>
            Third<int, int, string>(d.Get).Match(
                _ => $"{b}{a.Get}",
                _ => $"{c}{a.Get}",
                val => $"{val}{a.Get}"
            ).Run(result => Assert.Equal($"{d.Get}{a.Get}", result));
    }
}
