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

        [Property(DisplayName = "T1 is not equal to T2")]
        public void FalseT1NotT2(NonNull<string> a, int b)
        {
            First<string, int>(a.Get)
                .Equals(Second<string, int>(b))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "T2 is not equal to T1")]
        public void FalseT2NotT1(NonNull<string> a, int b)
        {
            Second<string, int>(b)
                .Equals(First<string, int>(a.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "T1 are not the same")]
        public void FalseT1(NonNull<string> a)
        {
            First<string, int>($"a{a.Get}")
                .Equals(First<string, int>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "T2 are not the same")]
        public void FalseT2(NonNull<string> a)
        {
            Second<int, string>($"a{a.Get}")
                .Equals(Second<int, string>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "T1 are equal")]
        public void TrueT1(NonNull<string> a)
        {
            First<string, int>(a.Get)
                .Equals(First<string, int>(a.Get))
                .Run(result => Assert.True(result));
        }

        [Property(DisplayName = "T2 are equal")]
        public void TrueT2(int a)
        {
            Second<string, int>(a)
                .Equals(Second<string, int>(a))
                .Run(result => Assert.True(result));
        }
    }
}
