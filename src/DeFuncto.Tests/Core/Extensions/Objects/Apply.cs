using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Extensions.Objects
{
    public class Apply
    {
        [Property(DisplayName = "Applies a function to an object")]
        public void Applies(NonNull<string> a) =>
            _ = a.Get.Apply(Ok<string, int>)
                .ShouldBeOk(a.Get);
    }
}
