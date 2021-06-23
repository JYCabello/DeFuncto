using Xunit;

namespace DeFuncto.Tests.Types.Result
{
    public class IsRight
    {
        [Fact(DisplayName = "Detects that is right correctly")]
        public void TrueOnRight()
        {
            var ok = Result<string, int>.Ok("banana");
            Assert.True(ok.IsOk);
        }


        [Fact(DisplayName = "Detects that is not right correctly")]
        public void FalseOnRight()
        {
            var error = Result<int, string>.Error("banana");
            Assert.False(error.IsOk);
        }
    }
}
