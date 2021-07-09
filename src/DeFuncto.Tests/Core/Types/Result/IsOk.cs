using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class IsRight
    {
        [Fact(DisplayName = "Detects that is right correctly")]
        public void TrueOnRight() =>
            Assert.True(Ok<string, int>("banana").IsOk);


        [Fact(DisplayName = "Detects that is not right correctly")]
        public void FalseOnRight() =>
            Assert.False(Error<int, string>("banana").IsOk);
    }
}
