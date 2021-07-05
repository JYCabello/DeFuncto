using System.Threading.Tasks;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncOption
{
    public class Match
    {
        [Fact(DisplayName = "Matches on Some")]
        public async Task OnSome()
        {
            AsyncOption<string> option = "banana";
            var result = await option.Match(Id, () => "pear");
            Assert.Equal("banana", result);
        }

        [Fact(DisplayName = "Matches on None")]
        public async Task OnNone()
        {
            AsyncOption<string> option = None.Option<string>();
            var result = await option.Match(Id, () => "banana");
            Assert.Equal("banana", result);
        }
    }
}
