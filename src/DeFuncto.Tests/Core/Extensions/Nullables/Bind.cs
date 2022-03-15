using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Nullables;

public class Bind
{
    [Property(DisplayName = "Binds the value and skips null")]
    public void Maps(int? value, int? anotherValue)
    {
        var result = value.Bind(v => anotherValue.Map(av => av + v));

        if (value is not null && anotherValue is not null)
        {
            Assert.NotNull(result);
            Assert.Equal(value.Value + anotherValue.Value, result!.Value);
        }

        if (value is null || anotherValue is null)
            Assert.Null(result);
    }
}
