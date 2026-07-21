using FsCheck;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.Result;

public class IsRight
{
    [Property(DisplayName = "Detects that is Ok correctly")]
    public void TrueOnOk(NonNull<string> a) =>
        Assert.True(Ok<string, int>("banana").IsOk);


    [Property(DisplayName = "Detects that is not Ok correctly")]
    public void FalseOnError(NonNull<string> a) =>
        Assert.False(Error<int, string>(a.Get).IsOk);
}
