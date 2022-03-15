using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du6;

public class Equals
{
    [Property(DisplayName = "Is not equal to null")]
    public void FalseByNull(NonNull<string> a) =>
        First<string, string, string, string, string, string>(a.Get)
            .Equals((object)null!)
            .Run(Assert.False);

    [Property(DisplayName = "First is not equal to First")]
    public void FirstNotEqualToFirst(NonNull<string> a) =>
        First<string, string, string, string, string, string>($"a{a.Get}")
            .Equals(First<string, string, string, string, string, string>($"b{a.Get}"))
            .Run(Assert.False);

    [Property(DisplayName = "First is not equal to Second")]
    public void FirstNotEqualToSecond(NonNull<string> a) =>
        First<string, string, string, string, string, string>(a.Get)
            .Equals(Second<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "First is not equal to Third")]
    public void FirstNotEqualToThird(NonNull<string> a) =>
        First<string, string, string, string, string, string>(a.Get)
            .Equals(Third<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "First is not equal to Fourth")]
    public void FirstNotEqualToFourth(NonNull<string> a) =>
        First<string, string, string, string, string, string>(a.Get)
            .Equals(Fourth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "First is not equal to Fifth")]
    public void FirstNotEqualToFifth(NonNull<string> a) =>
        First<string, string, string, string, string, string>(a.Get)
            .Equals(Fifth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "First is not equal to Sixth")]
    public void FirstNotEqualToSixth(NonNull<string> a) =>
        First<string, string, string, string, string, string>(a.Get)
            .Equals(Sixth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Second is not equal to Second")]
    public void SecondNotEqualToSecond(NonNull<string> a) =>
        Second<string, string, string, string, string, string>($"a{a.Get}")
            .Equals(Second<string, string, string, string, string, string>($"b{a.Get}"))
            .Run(Assert.False);

    [Property(DisplayName = "Second is not equal to Third")]
    public void SecondNotEqualToThird(NonNull<string> a) =>
        Second<string, string, string, string, string, string>(a.Get)
            .Equals(Third<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Second is not equal to Fourth")]
    public void SecondNotEqualToFourth(NonNull<string> a) =>
        Second<string, string, string, string, string, string>(a.Get)
            .Equals(Fourth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Second is not equal to Fifth")]
    public void SecondNotEqualToFifth(NonNull<string> a) =>
        Second<string, string, string, string, string, string>(a.Get)
            .Equals(Fifth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Second is not equal to Sixth")]
    public void SecondNotEqualToSixth(NonNull<string> a) =>
        Second<string, string, string, string, string, string>(a.Get)
            .Equals(Sixth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Third is not equal to Third")]
    public void ThirdNotEqualToThird(NonNull<string> a) =>
        Third<string, string, string, string, string, string>($"a{a.Get}")
            .Equals(Third<string, string, string, string, string, string>($"b{a.Get}"))
            .Run(Assert.False);

    [Property(DisplayName = "Third is not equal to Fourth")]
    public void ThirdNotEqualToFourth(NonNull<string> a) =>
        Third<string, string, string, string, string, string>(a.Get)
            .Equals(Fourth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Third is not equal to Fifth")]
    public void ThirdNotEqualToFifth(NonNull<string> a) =>
        Third<string, string, string, string, string, string>(a.Get)
            .Equals(Fifth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Third is not equal to Sixth")]
    public void ThirdNotEqualToSixth(NonNull<string> a) =>
        Third<string, string, string, string, string, string>(a.Get)
            .Equals(Sixth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Fourth is not equal to Fourth")]
    public void FourthNotEqualToFourth(NonNull<string> a) =>
        Fourth<string, string, string, string, string, string>($"a{a.Get}")
            .Equals(Fourth<string, string, string, string, string, string>($"b{a.Get}"))
            .Run(Assert.False);

    [Property(DisplayName = "Fourth is not equal to Fifth")]
    public void FourthNotEqualToFifth(NonNull<string> a) =>
        Fourth<string, string, string, string, string, string>(a.Get)
            .Equals(Fifth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Fourth is not equal to Sixth")]
    public void FourthNotEqualToSixth(NonNull<string> a) =>
        Fourth<string, string, string, string, string, string>(a.Get)
            .Equals(Sixth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Fifth is not equal to Fifth")]
    public void FifthNotEqualToFifth(NonNull<string> a) =>
        Fifth<string, string, string, string, string, string>($"a{a.Get}")
            .Equals(Fifth<string, string, string, string, string, string>($"b{a.Get}"))
            .Run(Assert.False);

    [Property(DisplayName = "Fifth is not equal to Sixth")]
    public void FifthNotEqualToSixth(NonNull<string> a) =>
        Fifth<string, string, string, string, string, string>(a.Get)
            .Equals(Sixth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.False);

    [Property(DisplayName = "Sixth is not equal to Sixth")]
    public void SixthNotEqualToSixth(NonNull<string> a) =>
        Sixth<string, string, string, string, string, string>($"a{a.Get}")
            .Equals(Sixth<string, string, string, string, string, string>($"b{a.Get}"))
            .Run(Assert.False);

    [Property(DisplayName = "First is equal to First")]
    public void FirstEqualToFirst(NonNull<string> a) =>
        First<string, string, string, string, string, string>(a.Get)
            .Equals(First<string, string, string, string, string, string>(a.Get))
            .Run(Assert.True);

    [Property(DisplayName = "Second is equal to Second")]
    public void SecondEqualToSecond(NonNull<string> a) =>
        Second<string, string, string, string, string, string>(a.Get)
            .Equals(Second<string, string, string, string, string, string>(a.Get))
            .Run(Assert.True);

    [Property(DisplayName = "Third is equal to Third")]
    public void ThirdEqualToThird(NonNull<string> a) =>
        Third<string, string, string, string, string, string>(a.Get)
            .Equals(Third<string, string, string, string, string, string>(a.Get))
            .Run(Assert.True);

    [Property(DisplayName = "Fourth is equal to Fourth")]
    public void FourthEqualToFourth(NonNull<string> a) =>
        Fourth<string, string, string, string, string, string>(a.Get)
            .Equals(Fourth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.True);

    [Property(DisplayName = "Fifth is equal to Fifth")]
    public void FifthEqualToFifth(NonNull<string> a) =>
        Fifth<string, string, string, string, string, string>(a.Get)
            .Equals(Fifth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.True);

    [Property(DisplayName = "Sixth is equal to Sixth")]
    public void SixthEqualToSixth(NonNull<string> a) =>
        Sixth<string, string, string, string, string, string>(a.Get)
            .Equals(Sixth<string, string, string, string, string, string>(a.Get))
            .Run(Assert.True);
}
