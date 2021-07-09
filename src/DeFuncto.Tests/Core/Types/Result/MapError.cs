using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class MapError
    {
        [Fact(DisplayName = "Maps the error")]
        public void MapsError() =>
            Error<int, string>("ba")
                .MapError(str => $"{str}nana")
                .ShouldBeError("banana");
    }
}
