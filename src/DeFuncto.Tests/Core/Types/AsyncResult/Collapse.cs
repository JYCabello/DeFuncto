using System.Threading.Tasks;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;
namespace DeFuncto.Tests.Core.Types.AsyncResult
{
    public class Collapse
    {
        [Fact(DisplayName = "Collapses on OK")]
        public Task OnOk() =>
            Ok<string, string>("banana")
                .Async()
                .Collapse()
                .Run(val => Assert.Equal("banana", val));

        [Fact(DisplayName = "Collapses on Error")]
        public Task OnError() =>
            Error<string, string>("banana")
                .Async()
                .Collapse()
                .Run(val => Assert.Equal("banana", val));
    }
}
