using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du;

public class GetHashCode
{
    [Property(DisplayName = "First is not equal to First")]
    public void FirstNotEqualToFirst(NonNull<string> a) =>
        Assert.NotEqual(
            First<string, string>($"a{a.Get}").GetHashCode(),
            First<string, string>($"b{a.Get}").GetHashCode()
        );

    [Property(DisplayName = "First is not equal to Second")]
    public void FirstNotEqualToSecond(NonNull<string> a, NonNull<string> b) =>
        Assert.NotEqual(
            First<string, string>(a.Get).GetHashCode(),
            Second<string, string>(b.Get).GetHashCode()
        );

    [Property(DisplayName = "Second is not equal to Second")]
    public void SecondNotEqualToSecond(NonNull<string> a) =>
        Assert.NotEqual(
            Second<string, string>($"a{a.Get}").GetHashCode(),
            Second<string, string>($"b{a.Get}").GetHashCode()
        );

    [Property(DisplayName = "First is equal to First")]
    public void FirstEqualToFirst(NonNull<string> a) =>
        Assert.Equal(
            First<string, string>(a.Get).GetHashCode(),
            First<string, string>(a.Get).GetHashCode()
        );

    [Property(DisplayName = "Second is equal to Second")]
    public void SecondEqualToSecond(NonNull<string> a) =>
        Assert.Equal(
            Second<string, string>(a.Get).GetHashCode(),
            Second<string, string>(a.Get).GetHashCode()
        );
}
