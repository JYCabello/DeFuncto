using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class Result
    {
        [Property(DisplayName = "Maps Some to Ok")]
        public void SomeToOk(string a, string b) =>
            _ = Some(a)
                .Async()
                .Result(42)
                .ShouldBeOk(b)
                .Result;

        [Property(DisplayName = "Maps None to Error")]
        public void NoneToError(NonNull<string> a) =>
           _= None.Option<int>()
                .Async()
                .Result(a.Get)
                .ShouldBeError(a.Get)
                .Result;
    }
}
