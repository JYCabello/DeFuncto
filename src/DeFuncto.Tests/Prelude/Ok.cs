using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Prelude
{
    public class Ok
    {
        [Fact(DisplayName = "Instantiates a ResultOk")]
        public void Works() =>
            Ok("banana").ToResult<int>().ShouldBeOk("banana");

        [Fact(DisplayName = "Instantiates a result that is Ok")]
        public void WorksWithBoth() =>
            Ok<string, int>("banana").ShouldBeOk("banana");
    }
}
