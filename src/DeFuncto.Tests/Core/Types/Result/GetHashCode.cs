using FsCheck;
using Xunit;
using static DeFuncto.Prelude;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.Result
{
    public class GetHashCode
    {
        [Property(DisplayName = "Ok are equal")]
        public void OkEqual(NonNull<string> a)
        {
            Assert.Equal(
                Ok<string, int>(a.Get).GetHashCode(),
                Ok<string, int>(a.Get).GetHashCode()
            );
        }

        [Property(DisplayName = "Error are equal")]
        public void ErrorEqual(int a)
        {
            Assert.Equal(
                Error<string, int>(a).GetHashCode(),
                Error<string, int>(a).GetHashCode()
            );
        }

        [Property(DisplayName = "Ok are not the same")]
        public void OkAreNotTheSame(NonNull<string> a)
        {
            Assert.NotEqual(
                Ok<string, int>($"a{a.Get}").GetHashCode(),
                Ok<string, int>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "Error are not the same")]
        public void ErrorAreNotTheSame(NonNull<string> a)
        {
            Assert.NotEqual(
                Error<int, string>($"a{a.Get}").GetHashCode(),
                Error<int, string>($"b{a.Get}").GetHashCode()
            );
        }

        [Property(DisplayName = "Ok is not equal to Error")]
        public void OkNotEqualToError(NonNull<string> a, int b)
        {
            Assert.NotEqual(
                Ok<string, int>(a.Get).GetHashCode(),
                Error<string, int>(b).GetHashCode()
            );
        }

        [Property(DisplayName = "Error is not equal to Ok")]
        public void ErrorNotEqualToOk(NonNull<string> a, int b)
        {
            Assert.NotEqual(
                Error<string, int>(b).GetHashCode(),
                Ok<string, int>(a.Get).GetHashCode()
            );
        }
    }
}
