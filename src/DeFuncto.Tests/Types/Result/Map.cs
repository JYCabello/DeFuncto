using System;
using Xunit;

namespace DeFuncto.Tests.Types.Either
{
    public class Map
    {
        [Fact(DisplayName = "Maps successfully")]
        public void MapsRight()
        {
            var ok = Result<string, int>.Ok("nana");
            var result = ok.Map(v => $"ba{v}");
            Assert.True(result.IsOk);
            Assert.NotNull(result.OkValue);
            Assert.Equal("banana", result.OkValue!);
        }

        [Fact(DisplayName = "Skips mapping")]
        public void SkipsLeft()
        {
            var error = Result<int, string>.Error("banana");
            var result = error.Map<string>(_ => throw new Exception("Should not throw"));
            Assert.True(result.IsError);
            Assert.NotNull(result.ErrorValue);
            Assert.Equal("banana", result.ErrorValue!);
        }
    }
}
