using System.Threading.Tasks;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;
namespace DeFuncto.Tests.Types.AsyncResult
{
    public class IsError
    {
        [Fact(DisplayName = "Confirms if it's an error")]
        public Task OnError() =>
            Error<int, string>("banana")
                .Async()
                .IsError
                .Run(Assert.True);

        [Fact(DisplayName = "Confirms if it's not an error")]
        public Task OnOk() =>
            Ok<string, int>("banana")
                .Async()
                .IsError
                .Run(Assert.False);
    }
}
