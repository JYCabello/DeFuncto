using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class Result
    {
        [Property(DisplayName = "Maps Some to Ok")]
        public void SomeToOk(NonNull<string> a) =>
            _ = Some(a.Get)
                .Async()
                .Result(42)
                .ShouldBeOk(a.Get)
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
