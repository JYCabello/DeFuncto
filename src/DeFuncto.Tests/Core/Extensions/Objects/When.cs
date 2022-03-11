using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

namespace DeFuncto.Tests.Core.Extensions.Objects;

public class When
{
    [Property(DisplayName = "It's some when the predicate evaluates to true")]
    public void True(NonNull<string> a) =>
        a.Get.When(val =>
        {
            Assert.Equal(a.Get, val);
            return true;
        }).ShouldBeSome(a.Get);

    [Property(DisplayName = "It's none when the predicate evaluates to false")]
    public void False(NonNull<string> a) =>
        a.Get.When(val =>
        {
            Assert.Equal(a.Get, val);
            return false;
        }).ShouldBeNone();
}
