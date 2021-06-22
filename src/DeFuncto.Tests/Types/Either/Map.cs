using System;
using Xunit;

namespace DeFuncto.Tests.Types.Either
{
    public class Map
    {
        [Fact(DisplayName = "Maps successfully")]
        public void MapsRight()
        {
            var right = Either<int, string>.Right("nana");
            var result = right.Map(v => $"ba{v}");
            Assert.True(result.IsRight);
            Assert.NotNull(result.Value);
            Assert.Equal("banana", result.Value!);
        }

        [Fact(DisplayName = "Skips mapping")]
        public void SkipsLeft()
        {
            var left = Either<string, int>.Left("banana");
            var result = left.Map<int>(_ => throw new Exception("Should not throw"));
            Assert.True(result.IsLeft);
            Assert.NotNull(result.Alternative);
            Assert.Equal("banana", result.Alternative!);
        }
    }
}
