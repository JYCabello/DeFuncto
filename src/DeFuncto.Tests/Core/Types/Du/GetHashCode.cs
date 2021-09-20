using FsCheck;
using DeFuncto.Extensions;
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
            First<string, int>(a.Get)
                .GetHashCode()
                .Run(result => Assert.Equal(First<string, int>(a.Get).GetHashCode(), result));
        }

        [Property(DisplayName = "Second are equal")]
        public void SecondEqual(int a)
        {
            Second<string, int>(a)
                .GetHashCode()
                .Run(result => Assert.Equal(Second<string, int>(a).GetHashCode(), result));
        }

        [Property(DisplayName = "First are not the same")]
        public void FirstAreNotTheSame(NonNull<string> a)
        {
            First<string, int>($"a{a.Get}")
                .GetHashCode()
                .Run(result => Assert.NotEqual(First<string, int>($"b{a.Get}").GetHashCode(), result));
        }

        [Property(DisplayName = "Second are not the same")]
        public void SecondAreNotTheSame(NonNull<string> a)
        {
            Second<int, string>($"a{a.Get}")
                .GetHashCode()
                .Run(result => Assert.NotEqual(Second<int, string>($"b{a.Get}").GetHashCode(), result));
        }

        [Property(DisplayName = "First is not equal to Second")]
        public void FirstNotEqualToSecond(NonNull<string> a, int b)
        {
            First<string, int>(a.Get)
                .GetHashCode()
                .Run(result => Assert.NotEqual(Second<string, int>(b).GetHashCode(), result));
        }

        [Property(DisplayName = "Second is not equal to First")]
        public void SecondNotEqualToFirst(NonNull<string> a, int b)
        {
            Second<string, int>(b)
                .GetHashCode()
                .Run(result => Assert.NotEqual(First<string, int>(a.Get).GetHashCode(), result));
        }
    }
}
