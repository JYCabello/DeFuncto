using FsCheck;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Equals
    {
        [Property(DisplayName = "Is not equal to null")]
        public void FalseByNull(NonNull<string> a)
        {
            Some(a.Get)
                .Equals((object)null)
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Some is not equal to None")]
        public void SomeNotEqualToNone(NonNull<string> a)
        {
            Some(a.Get)
                .Equals(Option<string>.None)
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "None is not equal to Some")]
        public void NoneNotEqualToSome(NonNull<string> a)
        {
            Option<string>.None.Equals(Some(a.Get))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Some are not the same")]
        public void SomeAreNotTheSame(NonNull<string> a)
        {
            Some($"a{a.Get}")
                .Equals(Some($"b{a.Get}"))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Some are not the same type")]
        public void SomeAreNotTheSameType(NonNull<string> a, int b)
        {
            Some(a.Get)
                .Equals(Some(b))
                .Run(result => Assert.False(result));
        }

        [Property(DisplayName = "Some are equal")]
        public void SomeEqual(NonNull<string> a)
        {
            Some(a.Get)
                .Equals(Some(a.Get))
                .Run(result => Assert.True(result));
        }

        [Fact(DisplayName = "None are equal")]
        public void NoneEqual()
        {
            Option<string>.None.Equals(Option<string>.None)
                .Run(result => Assert.True(result));
        }
    }
}
