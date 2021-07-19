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
            Ok(a).Result<int>().ShouldBeOk(a);

        [Property(DisplayName = "Instantiates a result that is Ok")]
        public void WorksWithBoth(NonNull<string> a) =>
            Ok<string, int>(a.Get).ShouldBeOk(a.Get);
    }
}
