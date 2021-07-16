using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Prelude
{
    public class Ok
    {
        [Property(DisplayName = "Instantiates a ResultOk")]
        public void Works(NonNull<string> a) =>
            Ok("banana").Result<int>().ShouldBeOk("banana");

        [Property(DisplayName = "Instantiates a result that is Ok")]
        public void WorksWithBoth(NonNull<string> a) =>
            Ok<string, int>("banana").ShouldBeOk("banana");
    }
}
