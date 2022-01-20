using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Du
{
    public class Iter
    {
        [Property]
        public void OnDu1Action(NonNull<string> a)
        {
            var witness = new Witness();

            new Du<string, int>(a.Get)
                .Iter((string _) => { witness.Touch(); });

            witness.ShouldHaveBeenTouched(1);
        }

        [Property]
        public void OnDu1Func(NonNull<string> a)
        {
            var witness = new Witness();

            new Du<string, int>(a.Get)
                .Iter((string _) =>
                {
                    witness.Touch();
                    return unit;
                });

            witness.ShouldHaveBeenTouched(1);
        }
    }
}
