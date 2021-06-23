using Xunit;

namespace DeFuncto.Tests.Types.Either
{
    public class IsLeft
    {
        [Fact(DisplayName = "Detects that is left correctly")]
        public void TrueOnLeft()
        {
            var error = Result<int, string>.Error("banana");
            Assert.True(error.IsError);
        }


        [Fact(DisplayName = "Detects that is not left correctly")]
        public void FalseOnRight()
        {
            var ok = Result<string, int>.Ok("banana");
            Assert.False(ok.IsError);
        }
    }
}
