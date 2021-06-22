using Xunit;

namespace DeFuncto.Tests.Types.Either
{
    public class IsLeft
    {
        [Fact(DisplayName = "Detects that is left correctly")]
        public void TrueOnLeft()
        {
            var right = Either<string, int>.Left("banana");
            Assert.True(right.IsLeft);
        }


        [Fact(DisplayName = "Detects that is not left correctly")]
        public void FalseOnRight()
        {
            var right = Either<int, string>.Right("banana");
            Assert.False(right.IsLeft);
        }
    }
}
