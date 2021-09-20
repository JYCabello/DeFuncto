using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Result
    {
        [Property(DisplayName = "Converts some into success result")]
        public void SomeToSuccess(NonNull<string> a, int b) =>
            Optional(a.Get)
                .Result(b)
                .ShouldBeOk(a.Get);

        [Property(DisplayName = "Converts none into error result")]
        public void NoneToEror(NonNull<string> a) =>
            Optional((int?) null)
                .Result(a.Get)
                .ShouldBeError(a.Get);
    }
}
