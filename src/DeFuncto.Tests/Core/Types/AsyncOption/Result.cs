using System.Threading.Tasks;
using DeFuncto.Assertions;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class Result
    {
        [Property(DisplayName = "Maps Some to Ok")]
        public void SomeToOk(string a, string b) =>
            Some(a)
                .Async()
                .Result(42)
                .ShouldBeOk(b);

        [Property(DisplayName = "Maps None to Error")]
        public Task NoneToError(string a) =>
            None.Option<int>()
                .Async()
                .Result(a)
                .ShouldBeError(a);
    }
}
