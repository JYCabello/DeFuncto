using FsCheck;
using Xunit;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

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

        [Fact(DisplayName = "None are equal")]
        public void NoneEqual()
        {
            Assert.Equal(
                 Option<string>.None.GetHashCode(),
                 Option<string>.None.GetHashCode()
            );
        }

        [Property(DisplayName = "None not equal to Some")]
        public void NoneNotEqualToSome(NonNull<string> a)
        {
            Assert.NotEqual(
                 Option<string>.None.GetHashCode(),
                 Some(a).GetHashCode()
            );
        }

        [Property(DisplayName = "Some not equal to None")]
        public void SomeNotEqualToNone(NonNull<string> a)
        {
            Assert.NotEqual(
                 Some(a).GetHashCode(),
                 Option<string>.None.GetHashCode()
            );
        }
    }
}
