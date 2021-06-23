using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Extensions.Objects
{
    public class Apply
    {
        [Fact(DisplayName = "Applies a function to an object")]
        public void Applies() =>
            "banana".Apply(Ok).ToResult<int>()
                .Run(r => Assert.True(r.IsOk))
                .Run(r => Assert.Equal("banana", r.OkValue));
    }
}
