using System;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    // Select happens to be an overload of map, needed for linq.
    public class MapAndSelect
    {
        [Fact(DisplayName = "Maps successfully")]
        public void MapsRight()
        {
            var ok = Ok<string, int>("nana");
            Test(ok.Map(Projection));
            Test(ok.Select(Projection));

            string Projection(string v) =>
                $"ba{v}";

            void Test(Result<string, int> result) =>
                result.ShouldBeOk("banana");
        }

        [Fact(DisplayName = "Skips mapping")]
        public void SkipsLeft()
        {
            var error = Error<int, string>("banana");
            Test(error.Map(ThrowingProjection));
            Test(error.Select(ThrowingProjection));

            int ThrowingProjection(int value) => throw new Exception("Should not throw");

            void Test(Result<int, string> result) =>
                result.ShouldBeError("banana");
        }
    }
}
