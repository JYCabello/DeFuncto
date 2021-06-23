using Xunit;
using static DeFuncto.Prelude;
namespace DeFuncto.Tests.Types.Result
{
    public class MapError
    {
        [Fact(DisplayName = "Maps the error")]
        public void MapsError()
        {
            var error = Error<int, string>("ba");
            var result = error.MapError(str => $"{str}nana");
            Assert.True(result.IsError);
            Assert.Equal("banana", result.ErrorValue);
        }
    }
}
