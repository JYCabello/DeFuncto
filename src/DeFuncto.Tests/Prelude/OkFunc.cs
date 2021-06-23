using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Prelude
{
    public class OkFunc
    {
        [Fact(DisplayName = "Instantiates a resultOk")]
        public void Works()
        {
            var ok = Ok("banana");
            Assert.True(ok.ToResult<int>().IsOk);
            Assert.Equal("banana", ok.OkValue);
        }
    }
}
