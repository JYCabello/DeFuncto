using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result;

public class IsLeft
{
    [Property(DisplayName = "Detects that is Error correctly")]
    public void TrueOnError(NonNull<string> a) =>
        Assert.True(Error<int, string>(a.Get).IsError);


    [Property(DisplayName = "Detects that is not Error correctly")]
    public void FalseOnOk(NonNull<string> a) =>
        Assert.False(Ok<string, int>(a.Get).IsError);
}
