using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Functions;

public class Actions
{
    [Fact(DisplayName = "Transforms a function into an action")]
    public void FunctionToAction()
    {
        var witness = new Witness();
        var func = () => witness.Touch();
        witness.ShouldHaveBeenTouched(0);
        var result = func.Action();
        witness.ShouldHaveBeenTouched(0);
        result();
        witness.ShouldHaveBeenTouched(1);
    }
}
