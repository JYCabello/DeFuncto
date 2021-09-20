using FsCheck;
using Xunit;
using static DeFuncto.Prelude;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class GetHashCode
    {
        [Property(DisplayName = "Some are equal")]
        public void SomeEqual(NonNull<string> a)
        {
            Assert.Equal(
                Some(a.Get).GetHashCode(),
                Some(a.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "Some are not the same")]
        public void SomeAreNotTheSame(NonNull<string> a)
        {
            Assert.NotEqual(
                Some($"a{a.Get}").GetHashCode(),
                Some($"b{a.Get}").GetHashCode()
            );
        }
    }
}
