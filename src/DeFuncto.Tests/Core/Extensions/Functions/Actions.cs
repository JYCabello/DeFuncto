using System;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Extensions.Functions
{
    public class Actions
    {
        [Fact(DisplayName = "Transforms a function into an action")]
        public void FunctionToAction()
        {
            var witness = new Witness();
            Func<Unit> func = () =>
            {
                witness.Touch();
                return unit;
            };
            witness.ShouldHaveBeenTouched(0);
            var result = func.Action();
            witness.ShouldHaveBeenTouched(0);
            result();
            witness.ShouldHaveBeenTouched(1);
        }
    }
}
