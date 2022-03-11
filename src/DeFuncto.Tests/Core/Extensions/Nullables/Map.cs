using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Nullables;

public class Map
{
    [Property(DisplayName = "Maps the value")]
    public void Maps(int value)
    {
        int? nullableValue = value;
        var result = nullableValue.Map(v => v + 1);
        Assert.NotNull(result);
        Assert.Equal(value + 1, result.Value);
    }

    [Fact(DisplayName = "Skips null")]
    public void Skips()
    {
        var result = ((int?) null).Map(v => v + 1);
        Assert.Null(result);
    }
}
