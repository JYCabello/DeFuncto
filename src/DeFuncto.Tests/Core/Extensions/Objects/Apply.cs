using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Extensions.Objects
{
    public class Apply
    {
        [Property(DisplayName = "Applies a function to an object")]
        public void Applies(string a) =>
            a.Apply(Ok<string, int>)
                .ShouldBeOk(a);
    }
}
