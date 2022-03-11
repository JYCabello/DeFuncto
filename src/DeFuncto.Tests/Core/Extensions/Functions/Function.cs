using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Functions;

public class Function
{
    [Property(DisplayName = "Creates a function that does the same")]
    public void Works(NonNull<string> a, NonNull<string> b)
    {
        var witness = a.Get;
        var touch = () =>
        {
            witness = b.Get;
        };
        Assert.Equal(a.Get, witness);
        var touchFunction = touch.Function();
        Assert.Equal(a.Get, witness);
        touchFunction();
        Assert.Equal(b.Get, witness);
    }
}
