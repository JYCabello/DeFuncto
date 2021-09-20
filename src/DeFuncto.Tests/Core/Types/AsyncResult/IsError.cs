using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;
namespace DeFuncto.Tests.Core.Types.AsyncResult
{
    public class IsError
    {
        [Property(DisplayName = "Confirms if it's an error")]
        public void OnError(string a) =>
            _ = Error<int, string>(a)
                .Async()
                .IsError
                .Run(Assert.True)
                .Result;

        [Property(DisplayName = "Confirms if it's not an error")]
        public void OnOk(string a) =>
            _ = Ok<string, int>("banana")
                .Async()
                .IsError
                .Run(Assert.False)
                .Result;
    }
}
