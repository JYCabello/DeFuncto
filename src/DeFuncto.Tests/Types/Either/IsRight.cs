using Xunit;

namespace DeFuncto.Tests.Types.Either
{
    public class IsRight
    {
        [Fact(DisplayName = "Detects that is right correctly")]
        public void TrueOnRight()
        {
            var right = Either<int, string>.Right("banana");
            Assert.True(right.IsRight);
        }


        [Fact(DisplayName = "Detects that is not right correctly")]
        public void FalseOnRight()
        {
            var right = Either<string, int>.Left("banana");
            Assert.False(right.IsRight);
        }
    }
}
