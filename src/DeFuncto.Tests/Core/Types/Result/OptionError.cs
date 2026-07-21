using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result;

public class OptionError
{
    [Property(DisplayName = "Turns Error into Some")]
    public void ErrorToSome(NonNull<string> a) =>
        Error<int, string>(a.Get)
            .OptionError
            .ShouldBeSome(a.Get);

    [Property(DisplayName = "Turns Ok into None")]
    public void OkToNone(NonNull<string> a) =>
        Ok<string, int>(a.Get)
            .OptionError
            .ShouldBeNone();
}
