using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.Result
{
    public class IsLeft
    {
        [Fact(DisplayName = "Detects that is left correctly")]
        public void TrueOnLeft() =>
            Assert.True(Error<int, string>("banana").IsError);


        [Fact(DisplayName = "Detects that is not left correctly")]
        public void FalseOnRight() =>
            Assert.False(Ok<string, int>("banana").IsError);
    }
}
