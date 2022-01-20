using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.Du
{
    public class Iter
    {
        [Property]
        public void OnDu1Action(NonNull<string> a)
        {
            var witness = new Witness();

            new Du<string, int>(a.Get)
                .Iter((string _) => witness.Touch());

            witness.ShouldHaveBeenTouched(1);
        }
    }
}
