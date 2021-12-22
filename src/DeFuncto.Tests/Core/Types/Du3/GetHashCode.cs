using FsCheck;
using Xunit;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du3
{
    public class GetHashCode
    {
        [Property(DisplayName = "First is not equal to First")]
        public void FirstNotEqualToFirst(NonNull<string> a)
        {
            Assert.NotEqual(
                First<string, string, string>($"a{a.Get}").GetHashCode(),
                First<string, string, string>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "First is not equal to Second")]
        public void FirstNotEqualToSecond(NonNull<string> a, NonNull<string> b)
        {
            Assert.NotEqual(
                First<string, string, string>(a.Get).GetHashCode(),
                Second<string, string, string>(b.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "First is not equal to Third")]
        public void FirstNotEqualToThird(NonNull<string> a, NonNull<string> b)
        {
            Assert.NotEqual(
                First<string, string, string>(a.Get).GetHashCode(),
                Third<string, string, string>(b.Get).GetHashCode()
            );
        }
        
        [Property(DisplayName = "Second is not equal to Second")]
        public void SecondNotEqualToSecond(NonNull<string> a)
        {
            Assert.NotEqual(
                Second<string, string, string>($"a{a.Get}").GetHashCode(),
                Second<string, string, string>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "Second is not equal to Third")]
        public void SecondNotEqualToThird(NonNull<string> a, NonNull<string> b)
        {
            Assert.NotEqual(
                Second<string, string, string>(a.Get).GetHashCode(),
                Third<string, string, string>(b.Get).GetHashCode()
            );
        }
       
        [Property(DisplayName = "Third is not equal to Third")]
        public void ThirdNotEqualToThird(NonNull<string> a)
        {
            Assert.NotEqual(
                Third<string, string, string>($"a{a.Get}").GetHashCode(),
                Third<string, string, string>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "First is equal to First")]
        public void FirstEqualToFirst(NonNull<string> a)
        {
            Assert.Equal(
                First<string, string, string>(a.Get).GetHashCode(),
                First<string, string, string>(a.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "Second is equal to Second")]
        public void SecondEqualToSecond(NonNull<string> a)
        {
            Assert.Equal(
                Second<string, string, string>(a.Get).GetHashCode(),
                Second<string, string, string>(a.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "Third is equal to Third")]
        public void ThirdEqualToThird(NonNull<string> a)
        {
            Assert.Equal(
                Third<string, string, string>(a.Get).GetHashCode(),
                Third<string, string, string>(a.Get).GetHashCode()
            );
        }
    }
}
