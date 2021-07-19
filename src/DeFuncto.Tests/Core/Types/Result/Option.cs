using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class Option
    {
        [Property(DisplayName = "Turns Ok into Some")]
        public void OkToSome(NonNull<string> a) =>
            Ok<string, int>(a.Get)
                .Option
                .ShouldBeSome(a.Get);

        [Property(DisplayName = "Turns Error into None")]
        public void ErrorToNone(NonNull<string> a) =>
            Error<int, string>(a.Get)
                .Option
                .ShouldBeNone();
    }
}
