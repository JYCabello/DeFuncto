using FsCheck;
using Xunit;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du4
{
    public class GetHashCode
    {
        [Property(DisplayName = "First is not equal to First")]
        public void FirstNotEqualToFirst(NonNull<string> a)
        {
            Assert.NotEqual(
                First<string, string, string, string>($"a{a.Get}").GetHashCode(),
                First<string, string, string, string>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "First is not equal to Second")]
        public void FirstNotEqualToSecond(NonNull<string> a, NonNull<string> b)
        {
            Assert.NotEqual(
                First<string, string, string, string>(a.Get).GetHashCode(),
                Second<string, string, string, string>(b.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "First is not equal to Third")]
        public void FirstNotEqualToThird(NonNull<string> a, NonNull<string> b)
        {
            Assert.NotEqual(
                First<string, string, string, string>(a.Get).GetHashCode(),
                Third<string, string, string, string>(b.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "First is not equal to Fourth")]
        public void FirstNotEqualToFourth(NonNull<string> a, NonNull<string> b)
        {
            Assert.NotEqual(
                First<string, string, string, string>(a.Get).GetHashCode(),
                Fourth<string, string, string, string>(b.Get).GetHashCode()
            );
        }
        
        [Property(DisplayName = "Second is not equal to Second")]
        public void SecondNotEqualToSecond(NonNull<string> a)
        {
            Assert.NotEqual(
                Second<string, string, string, string>($"a{a.Get}").GetHashCode(),
                Second<string, string, string, string>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "Second is not equal to Third")]
        public void SecondNotEqualToThird(NonNull<string> a, NonNull<string> b)
        {
            Assert.NotEqual(
                Second<string, string, string, string>(a.Get).GetHashCode(),
                Third<string, string, string, string>(b.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "Second is not equal to Fourth")]
        public void SecondNotEqualToFourth(NonNull<string> a, NonNull<string> b)
        {
            Assert.NotEqual(
                Second<string, string, string, string>(a.Get).GetHashCode(),
                Fourth<string, string, string, string>(b.Get).GetHashCode()
            );
        }
       
        [Property(DisplayName = "Third is not equal to Third")]
        public void ThirdNotEqualToThird(NonNull<string> a)
        {
            Assert.NotEqual(
                Third<string, string, string, string>($"a{a.Get}").GetHashCode(),
                Third<string, string, string, string>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "Third is not equal to Fourth")]
        public void ThirdNotEqualToFourth(NonNull<string> a, NonNull<string> b)
        {
            Assert.NotEqual(
                Third<string, string, string, string>(a.Get).GetHashCode(),
                Fourth<string, string, string, string>(b.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "Fourth is not equal to Fourth")]
        public void FourthNotEqualToFourth(NonNull<string> a)
        {
            Assert.NotEqual(
                Fourth<string, string, string, string>($"a{a.Get}").GetHashCode(),
                Fourth<string, string, string, string>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "First is equal to First")]
        public void FirstEqualToFirst(NonNull<string> a)
        {
            Assert.Equal(
                First<string, string, string, string>(a.Get).GetHashCode(),
                First<string, string, string, string>(a.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "Second is equal to Second")]
        public void SecondEqualToSecond(NonNull<string> a)
        {
            Assert.Equal(
                Second<string, string, string, string>(a.Get).GetHashCode(),
                Second<string, string, string, string>(a.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "Third is equal to Third")]
        public void ThirdEqualToThird(NonNull<string> a)
        {
            Assert.Equal(
                Third<string, string, string, string>(a.Get).GetHashCode(),
                Third<string, string, string, string>(a.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "Fourth is equal to Fourth")]
        public void FourthEqualToFourth(NonNull<string> a)
        {
            Assert.Equal(
                Fourth<string, string, string, string>(a.Get).GetHashCode(),
                Fourth<string, string, string, string>(a.Get).GetHashCode()
            );
        }
    }
}
