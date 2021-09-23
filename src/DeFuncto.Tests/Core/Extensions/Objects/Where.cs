using FsCheck;
using FsCheck.Xunit;
using System.Linq;
using DeFuncto.Extensions;
using DeFuncto.Assertions;

namespace DeFuncto.Tests.Core.Extensions.Objects
{
    public class Where
    {
        [Property]
        public void Some(NonNull<string> a)
        {
            a.Where(x => x == a).ShouldBeSome(a);
        }

        [Property]
        public void None(NonNull<string> a)
        {
            a.Where(x => x != a).ShouldBeNone();
        }
    }
}
