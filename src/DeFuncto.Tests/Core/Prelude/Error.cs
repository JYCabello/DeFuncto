using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Prelude
{
    public class Error
    {
        [Fact(DisplayName = "Instantiates a ResultError")]
        public void Works() =>
            Error("banana").Result<int>()
                .ShouldBeError("banana");

        [Fact(DisplayName = "Instantiates a Result that is an Error")]
        public void WorksWithBoth() =>
            Error<int, string>("banana")
                .ShouldBeError("banana");
    }
}
