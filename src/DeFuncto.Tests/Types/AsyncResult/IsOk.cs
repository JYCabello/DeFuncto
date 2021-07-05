using System.Threading.Tasks;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class IsOk
    {
        [Fact(DisplayName = "Confirms if it's Ok")]
        public Task OnError() =>
            Error<int, string>("banana")
                .Async()
                .IsOk
                .Run(Assert.False);

        [Fact(DisplayName = "Confirms if it's not Ok")]
        public Task OnOk() =>
            Ok<string, int>("banana")
                .Async()
                .IsOk
                .Run(Assert.True);
    }
}
