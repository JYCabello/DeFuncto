using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Iter
    {
        [Fact(DisplayName = "Iterates on some")]
        public void OnSome()
        {
            var witness = new Witness();

            Some("banana")
                .Iter(_ => witness.Touch())
                .Iter(_ => witness.Touch(), () => witness.Touch());
            None.Option<string>()
                .Iter(_ => witness.Touch())
                .Iter(_ => witness.Touch(), () => witness.Touch());

            witness.ShouldHaveBeenTouched(3);
        }

        [Fact(DisplayName = "Iterates on none")]
        public void OnNone()
        {
            var witness = new Witness();

            Some("banana")
                .Iter(() => witness.Touch())
                .Iter(_ => witness.Touch(), () => witness.Touch());
            None.Option<string>()
                .Iter(() => witness.Touch())
                .Iter(_ => witness.Touch(), () => witness.Touch());

            witness.ShouldHaveBeenTouched(3);
        }
    }
}
