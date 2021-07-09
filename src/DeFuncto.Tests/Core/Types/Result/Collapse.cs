using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class Collapse
    {
        [Fact(DisplayName = "Collapses OK")]
        public void OkValue() => Assert.Equal("banana", Ok<string, string>("banana").Collapse());

        [Fact(DisplayName = "Collapses Error")]
        public void ErrorValue() => Assert.Equal("banana", Error<string, string>("banana").Collapse());
    }
}
