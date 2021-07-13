using System;
using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    // Select happens to be an overload of map, needed for linq.
    public class MapAndSelect
    {
        [Property(DisplayName = "Maps successfully")]
        public void MapsRight(NonNull<string> a, NonNull<string> b)
        {
            var ok = Ok<string, int>(a.Get);
            Test(ok.Map(Projection));
            Test(ok.Select(Projection));

            string Projection(string v) =>
                $"{b.Get}{v}";

            void Test(Result<string, int> result) =>
                result.ShouldBeOk($"{b.Get}{a.Get}");
        }

        [Property(DisplayName = "Skips mapping")]
        public void SkipsLeft(NonNull<string> a)
        {
            var error = Error<int, string>(a.Get);
            Test(error.Map(ThrowingProjection));
            Test(error.Select(ThrowingProjection));

            int ThrowingProjection(int value) => throw new Exception("Should not throw");

            void Test(Result<int, string> result) =>
                result.ShouldBeError(a.Get);
        }
    }
}
