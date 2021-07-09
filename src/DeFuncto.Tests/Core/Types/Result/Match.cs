using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class Match
    {
        [Fact(DisplayName = "Matches with OK")]
        public void WithOk()
        {
            var result = Ok<string, int>("nana")
                .Match(
                    ok => $"ba{ok}",
                    error => (error / error - 1 + 42).ToString()
                );
            Assert.Equal("banana", result);
        }

        [Fact(DisplayName = "Matches with Error")]
        public void WithError()
        {
            var result = Error<int, string>("nana")
                .Match(
                    ok => (ok / ok - 1 + 42).ToString(),
                    error => $"ba{error}"
                );
            Assert.Equal("banana", result);
        }
    }
}
