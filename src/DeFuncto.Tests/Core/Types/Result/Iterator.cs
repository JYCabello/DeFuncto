using System.Linq;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result;

public class Iterator
{
    [Fact(DisplayName = "Iterates over ok")]
    public void IteratesOk()
    {
        var result = Ok<int, string>(1);
        var count = 0;
        foreach (var value in result)
        {
            Assert.Equal(1, value);
            count++;
        }
        Assert.Equal(1, count);
    }

    [Fact(DisplayName = "Does not iterate over error")]
    public void DoesNotIterateError()
    {
        var result = Error<int, string>("nope");
        var count = result.Count();
        Assert.Equal(0, count);
    }
}
