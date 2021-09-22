﻿using FsCheck;
using FsCheck.Xunit;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du7
{
    public class Equals
    {
        [Property(DisplayName = "Is not equal to null")]
        public void FalseByNull(NonNull<string> a)
        {
            First<string, string, string, string, string, string, string>(a.Get)
                .Equals((object)null)
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "First is not equal to First")]
        public void FirstNotEqualToFirst(NonNull<string> a)
        {
            First<string, string, string, string, string, string, string>($"a{a.Get}")
                .Equals(First<string, string, string, string, string, string, string>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "First is not equal to Second")]
        public void FirstNotEqualToSecond(NonNull<string> a, NonNull<string> b)
        {
            First<string, string, string, string, string, string, string>(a.Get)
                .Equals(Second<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "First is not equal to Third")]
        public void FirstNotEqualToThird(NonNull<string> a, NonNull<string> b)
        {
            First<string, string, string, string, string, string, string>(a.Get)
                .Equals(Third<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }
        
        [Property(DisplayName = "First is not equal to Fourth")]
        public void FirstNotEqualToFourth(NonNull<string> a, NonNull<string> b)
        {
            First<string, string, string, string, string, string, string>(a.Get)
                .Equals(Fourth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }
        
        [Property(DisplayName = "First is not equal to Fifth")]
        public void FirstNotEqualToFifth(NonNull<string> a, NonNull<string> b)
        {
            First<string, string, string, string, string, string, string>(a.Get)
                .Equals(Fifth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }
        
        [Property(DisplayName = "First is not equal to Sixth")]
        public void FirstNotEqualToSixth(NonNull<string> a, NonNull<string> b)
        {
            First<string, string, string, string, string, string, string>(a.Get)
                .Equals(Sixth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }
        
        [Property(DisplayName = "First is not equal to Seventh")]
        public void FirstNotEqualToSeventh(NonNull<string> a, NonNull<string> b)
        {
            First<string, string, string, string, string, string, string>(a.Get)
                .Equals(Seventh<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Second is not equal to Second")]
        public void SecondNotEqualToSecond(NonNull<string> a)
        {
            Second<string, string, string, string, string, string, string>($"a{a.Get}")
                .Equals(Second<string, string, string, string, string, string, string>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Second is not equal to Third")]
        public void SecondNotEqualToThird(NonNull<string> a, NonNull<string> b)
        {
            Second<string, string, string, string, string, string, string>(a.Get)
                .Equals(Third<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Second is not equal to Fourth")]
        public void SecondNotEqualToFourth(NonNull<string> a, NonNull<string> b)
        {
            Second<string, string, string, string, string, string, string>(a.Get)
                .Equals(Fourth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Second is not equal to Fifth")]
        public void SecondNotEqualToFifth(NonNull<string> a, NonNull<string> b)
        {
            Second<string, string, string, string, string, string, string>(a.Get)
                .Equals(Fifth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Second is not equal to Sixth")]
        public void SecondNotEqualToSixth(NonNull<string> a, NonNull<string> b)
        {
            Second<string, string, string, string, string, string, string>(a.Get)
                .Equals(Sixth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Second is not equal to Seventh")]
        public void SecondNotEqualToSeventh(NonNull<string> a, NonNull<string> b)
        {
            Second<string, string, string, string, string, string, string>(a.Get)
                .Equals(Seventh<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Third is not equal to Third")]
        public void ThirdNotEqualToThird(NonNull<string> a)
        {
            Third<string, string, string, string, string, string, string>($"a{a.Get}")
                .Equals(Third<string, string, string, string, string, string, string>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Third is not equal to Fourth")]
        public void ThirdNotEqualToFourth(NonNull<string> a, NonNull<string> b)
        {
            Third<string, string, string, string, string, string, string>(a.Get)
                .Equals(Fourth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Third is not equal to Fifth")]
        public void ThirdNotEqualToFifth(NonNull<string> a, NonNull<string> b)
        {
            Third<string, string, string, string, string, string, string>(a.Get)
                .Equals(Fifth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Third is not equal to Sixth")]
        public void ThirdNotEqualToSixth(NonNull<string> a, NonNull<string> b)
        {
            Third<string, string, string, string, string, string, string>(a.Get)
                .Equals(Sixth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Third is not equal to Seventh")]
        public void ThirdNotEqualToSeventh(NonNull<string> a, NonNull<string> b)
        {
            Third<string, string, string, string, string, string, string>(a.Get)
                .Equals(Seventh<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Fourth is not equal to Fourth")]
        public void FourthNotEqualToFourth(NonNull<string> a)
        {
            Fourth<string, string, string, string, string, string, string>($"a{a.Get}")
                .Equals(Fourth<string, string, string, string, string, string, string>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Fourth is not equal to Fifth")]
        public void FourthNotEqualToFifth(NonNull<string> a, NonNull<string> b)
        {
            Fourth<string, string, string, string, string, string, string>(a.Get)
                .Equals(Fifth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Fourth is not equal to Sixth")]
        public void FourthNotEqualToSixth(NonNull<string> a, NonNull<string> b)
        {
            Fourth<string, string, string, string, string, string, string>(a.Get)
                .Equals(Sixth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Fourth is not equal to Seventh")]
        public void FourthNotEqualToSeventh(NonNull<string> a, NonNull<string> b)
        {
            Fourth<string, string, string, string, string, string, string>(a.Get)
                .Equals(Seventh<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Fifth is not equal to Fifth")]
        public void FifthNotEqualToFifth(NonNull<string> a)
        {
            Fifth<string, string, string, string, string, string, string>($"a{a.Get}")
                .Equals(Fifth<string, string, string, string, string, string, string>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Fifth is not equal to Sixth")]
        public void FifthNotEqualToSixth(NonNull<string> a, NonNull<string> b)
        {
            Fifth<string, string, string, string, string, string, string>(a.Get)
                .Equals(Sixth<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Fifth is not equal to Seventh")]
        public void FifthNotEqualToSeventh(NonNull<string> a, NonNull<string> b)
        {
            Fifth<string, string, string, string, string, string, string>(a.Get)
                .Equals(Seventh<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Sixth is not equal to Sixth")]
        public void SixthNotEqualToSixth(NonNull<string> a)
        {
            Sixth<string, string, string, string, string, string, string>($"a{a.Get}")
                .Equals(Sixth<string, string, string, string, string, string, string>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Sixth is not equal to Seventh")]
        public void SixthNotEqualToSeventh(NonNull<string> a, NonNull<string> b)
        {
            Sixth<string, string, string, string, string, string, string>(a.Get)
                .Equals(Seventh<string, string, string, string, string, string, string>(b.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Seventh is not equal to Seventh")]
        public void SeventhNotEqualToSeventh(NonNull<string> a)
        {
            Seventh<string, string, string, string, string, string, string>($"a{a.Get}")
                .Equals(Seventh<string, string, string, string, string, string, string>($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "First is equal to First")]
        public void FirstEqualToFirst(NonNull<string> a)
        {
            First<string, string, string, string, string, string, string>(a.Get)
                .Equals(First<string, string, string, string, string, string, string>(a.Get))
                .Run(result => Assert.True(result));
        }

        [Property(DisplayName = "Second is equal to Second")]
        public void SecondEqualToSecond(NonNull<string> a)
        {
            Second<string, string, string, string, string, string, string>(a.Get)
                .Equals(Second<string, string, string, string, string, string, string>(a.Get))
                .Run(result => Assert.True(result));
        }

        [Property(DisplayName = "Third is equal to Third")]
        public void ThirdEqualToThird(NonNull<string> a)
        {
            Third<string, string, string, string, string, string, string>(a.Get)
                .Equals(Third<string, string, string, string, string, string, string>(a.Get))
                .Run(result => Assert.True(result));
        }

        [Property(DisplayName = "Fourth is equal to Fourth")]
        public void FourthEqualToFourth(NonNull<string> a)
        {
            Fourth<string, string, string, string, string, string, string>(a.Get)
                .Equals(Fourth<string, string, string, string, string, string, string>(a.Get))
                .Run(result => Assert.True(result));
        }

        [Property(DisplayName = "Fifth is equal to Fifth")]
        public void FifthEqualToFifth(NonNull<string> a)
        {
            Fifth<string, string, string, string, string, string, string>(a.Get)
                .Equals(Fifth<string, string, string, string, string, string, string>(a.Get))
                .Run(result => Assert.True(result));
        }

        [Property(DisplayName = "Sixth is equal to Sixth")]
        public void SixthEqualToSixth(NonNull<string> a)
        {
            Sixth<string, string, string, string, string, string, string>(a.Get)
                .Equals(Sixth<string, string, string, string, string, string, string>(a.Get))
                .Run(result => Assert.True(result));
        }

        [Property(DisplayName = "Seventh is equal to Seventh")]
        public void SeventhEqualToSeventh(NonNull<string> a)
        {
            Seventh<string, string, string, string, string, string, string>(a.Get)
                .Equals(Seventh<string, string, string, string, string, string, string>(a.Get))
                .Run(result => Assert.True(result));
        }
    }
}