using DeFuncto.Assertions;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Prelude;

public class Optional
{
    [Fact(DisplayName = "Null returns None")]
    public void NoneByNullable() =>
        Optional((int?)null).ShouldBeNone();

    [Property(DisplayName = "None null returns Some")]
    public void SomeByNullable(int a) =>
        Optional((int?)a).ShouldBeSome();
}
