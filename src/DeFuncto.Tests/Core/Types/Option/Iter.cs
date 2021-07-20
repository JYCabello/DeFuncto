using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Iter
    {
        [Property(DisplayName = "Iterates on some")]
        public void OnSome(NonNull<string> a)
        {
            var witness = new Witness();

            Some(a.Get)
                .Iter(_ => witness.Touch())
                .Iter(_ => witness.Touch(), () => witness.Touch());
            None.Option<string>()
                .Iter(_ => witness.Touch())
                .Iter(_ => witness.Touch(), () => witness.Touch());

            witness.ShouldHaveBeenTouched(3);
        }

        [Property(DisplayName = "Iterates on none")]
        public void OnNone(NonNull<string> a)
        {
            var witness = new Witness();

            Some(a.Get)
                .Iter(() => witness.Touch())
                .Iter(_ => witness.Touch(), () => witness.Touch());
            None.Option<string>()
                .Iter(() => witness.Touch())
                .Iter(_ => witness.Touch(), () => witness.Touch());

            witness.ShouldHaveBeenTouched(3);
        }
    }
}
