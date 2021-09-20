using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Match
    {
        [Property(DisplayName = "Matches on some")]
        public void OnSome(NonNull<string> a, NonNull<string> b, NonNull<string> c) =>
            Some(a.Get)
                .Match(val => val + b.Get, () => c.Get)
                .Run(val => Assert.Equal(a.Get + b.Get, val));

        [Property(DisplayName = "Matches on none")]
        public void OnNone(NonNull<string> a, NonNull<string> b) =>
            None
                .Option<string>()
                .Match(val => val + a.Get, () => b.Get)
                .Run(val => Assert.Equal(b.Get, val));
    }
}
