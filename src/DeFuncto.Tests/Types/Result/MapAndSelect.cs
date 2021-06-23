using System;
using Xunit;

namespace DeFuncto.Tests.Types.Result
{
    // Select happens to be an overload of map, needed for linq.
    public class MapAndSelect
    {
        [Fact(DisplayName = "Maps successfully")]
        public void MapsRight()
        {
            var ok = Result<string, int>.Ok("nana");
            Test(ok.Map(Projection));
            Test(ok.Select(Projection));

            string Projection(string v) =>
                $"ba{v}";

            void Test(Result<string, int> result)
            {
                Assert.True(result.IsOk);
                Assert.NotNull(result.OkValue);
                Assert.Equal("banana", result.OkValue!);
            }
        }

        [Fact(DisplayName = "Skips mapping")]
        public void SkipsLeft()
        {
            var error = Result<int, string>.Error("banana");
            Test(error.Map(ThrowingProjection));
            Test(error.Select(ThrowingProjection));

            int ThrowingProjection(int value) => throw new Exception("Should not throw");

            void Test(Result<int, string> result)
            {
                Assert.True(result.IsError);
                Assert.NotNull(result.ErrorValue);
                Assert.Equal("banana", result.ErrorValue!);
            }
        }
    }
}
