using FsCheck;
using Xunit;
using static DeFuncto.Prelude;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.Du
{
    public class GetHashCode
    {
        [Property(DisplayName = "First are equal")]
        public void FirstEqual(NonNull<string> a)
        {
            Assert.Equal(
                First<string, int>(a.Get).GetHashCode(),
                First<string, int>(a.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "Second are equal")]
        public void SecondEqual(int a)
        {
            Assert.Equal(
                Second<string, int>(a).GetHashCode(),
                Second<string, int>(a).GetHashCode()
            );
        }

        [Property(DisplayName = "First are not the same")]
        public void FirstAreNotTheSame(NonNull<string> a)
        {
            Assert.NotEqual(
                First<string, int>($"a{a.Get}").GetHashCode(),
                First<string, int>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "Second are not the same")]
        public void SecondAreNotTheSame(NonNull<string> a)
        {
            Assert.NotEqual(
                Second<int, string>($"a{a.Get}").GetHashCode(),
                Second<int, string>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "First is not equal to Second")]
        public void FirstNotEqualToSecond(NonNull<string> a, int b)
        {
            Assert.NotEqual(
                First<string, int>(a.Get).GetHashCode(),
                Second<string, int>(b).GetHashCode()
            );
        }

        [Property(DisplayName = "Second is not equal to First")]
        public void SecondNotEqualToFirst(NonNull<string> a, int b)
        {
            Assert.NotEqual(
                Second<string, int>(b).GetHashCode(),
                First<string, int>(a.Get).GetHashCode()
            );
        }
    }
}
