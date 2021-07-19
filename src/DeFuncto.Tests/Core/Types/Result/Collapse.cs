using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class Collapse
    {
        [Property(DisplayName = "Collapses OK")]
        public void OkValue(NonNull<string> a) =>
            Assert.Equal(a.Get, Ok<string, string>(a.Get).Collapse());

        [Property(DisplayName = "Collapses Error")]
        public void ErrorValue(NonNull<string> a) =>
            Assert.Equal(a.Get, Error<string, string>(a.Get).Collapse());
    }
}
