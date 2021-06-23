using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Prelude
{
    public class Ok
    {
        [Fact(DisplayName = "Instantiates a ResultOk")]
        public void Works()
        {
            var ok = Ok("banana");
            Assert.True(ok.ToResult<int>().IsOk);
            Assert.Equal("banana", ok.OkValue);
        }

        [Fact(DisplayName = "Instantiates a result that is Ok")]
        public void WorksWithBoth()
        {
            var ok = Ok<string, int>("banana");
            Assert.True(ok.IsOk);
            Assert.Equal("banana", ok.OkValue);
        }
    }
}
