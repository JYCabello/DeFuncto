using FsCheck;
using FsCheck.Xunit;
using DeFuncto.Extensions;
using DeFuncto.Assertions;

namespace DeFuncto.Tests.Core.Extensions.Objects
{
    public class Predicate
    {
        [Property(DisplayName = "Some if true")]
        public void Some(NonNull<string> a)
        {
            a.Predicate(x => x == a).ShouldBeSome(a);
        }

        [Property(DisplayName = "None if false")]
        public void None(NonNull<string> a)
        {
            a.Predicate(x => x != a).ShouldBeNone();
        }
    }
}
