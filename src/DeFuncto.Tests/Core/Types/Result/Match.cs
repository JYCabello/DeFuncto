using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class Match
    {
        [Property(DisplayName = "Matches with OK")]
        public void WithOk(NonNull<string> a, NonNull<string> b, int c)
        {
            var result = Ok<string, int>(a.Get)
                .Match(
                    ok => $"{b.Get}{ok}",
                    error => (error + c).ToString()
                );
            Assert.Equal($"{b.Get}{a.Get}", result);
        }

        [Property(DisplayName = "Matches with Error")]
        public void WithError(NonNull<string> a, NonNull<string> b, int c)
        {
            var result = Error<int, string>(a.Get)
                .Match(
                    ok => (ok + c).ToString(),
                    error => $"{b.Get}{error}"
                );
            Assert.Equal($"{b.Get}{a.Get}", result);
        }
    }
}
