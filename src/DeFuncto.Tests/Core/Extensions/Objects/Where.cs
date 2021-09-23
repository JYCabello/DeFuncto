using FsCheck;
using FsCheck.Xunit;
using DeFuncto.Extensions;
using DeFuncto.Assertions;

namespace DeFuncto.Tests.Core.Extensions.Objects
{
    public class Where
    {
        [Property(DisplayName = "Some if true")]
        public void Some(string a) =>
            a.Where(_ => true).ShouldBeSome(a);

        [Property(DisplayName = "None if false")]
        public void None(string a) =>
            a.Where(_ => false).ShouldBeNone();
    }
}
