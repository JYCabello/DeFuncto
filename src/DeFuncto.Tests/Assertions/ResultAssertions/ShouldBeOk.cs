using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Assertions.ResultAssertions;

public class ShouldBeOk
{
    [Property(DisplayName = "Fails when it's not OK")]
    public void FailsNotOk(NonNull<string> a)
    {
        var error = Error<int, string>(a.Get).Async();
        _ = Assert.ThrowsAsync<AssertionFailedException>(() => error.ShouldBeOk()).Result;
    }

    [Property(DisplayName = "Fails when it's not the same value for OK")]
    public void FailsNotSameOk(NonNull<string> a, NonNull<string> b)
    {
        var ok = Ok<string, int>(a.Get).Async();
        _ = ok.ShouldBeOk(a.Get).Result;
        if (a.Get != b.Get)
            _ = Assert.ThrowsAsync<AssertionFailedException>(() => ok.ShouldBeOk(b.Get)).Result;
    }
}
