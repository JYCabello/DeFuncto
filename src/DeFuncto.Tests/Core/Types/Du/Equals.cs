using FsCheck;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.Du
{
    public class Equals
    {
        [Property(DisplayName = "Is not equal to null")]
        public void FalseByNull(NonNull<string> a)
        {
            First<string, int>(a.Get)
                .Equals((object)null)
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "First is not equal to Second")]
        public void FirstNotEqualToSecond(NonNull<string> a, int b)
        {
            First<string, int>(a.Get)
                .Equals(Second<string, int>(b))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Second is not equal to First")]
        public void SecondNotEqualToFirst(NonNull<string> a, int b)
        {
            Second<string, int>(b)
                .Equals(First<string, int>(a.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "First are not the same")]
        public void FirstAreNotTheSame(NonNull<string> a)
        {
            First<string, int>($"a{a.Get}")
                .Equals(First<string, int>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Second are not the same")]
        public void SecondAreNotTheSame(NonNull<string> a)
        {
            Second<int, string>($"a{a.Get}")
                .Equals(Second<int, string>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "First are equal")]
        public void FirstEqual(NonNull<string> a)
        {
            First<string, int>(a.Get)
                .Equals(First<string, int>(a.Get))
                .Run(result => Assert.True(result));
        }

        [Property(DisplayName = "Second are equal")]
        public void SecondEqual(int a)
        {
            Second<string, int>(a)
                .Equals(Second<string, int>(a))
                .Run(result => Assert.True(result));
        }
    }
}
