using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Async
    {
        [Fact(DisplayName = "From option of a task")]
        public Task OptionOfTask() =>
            Some("banana".ToTask())
                .Async()
                .ShouldBeSome("banana");
    }
}
