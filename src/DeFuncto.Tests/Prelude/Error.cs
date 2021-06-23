using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Prelude
{
    public class Error
    {
        [Fact(DisplayName = "Instantiates a ResultError")]
        public void Works()
        {
            var error = Error("banana");
            Assert.True(error.ToResult<int>().IsError);
            Assert.Equal("banana", error.ErrorValue);
        }
    }
}
