using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option;

public class Iter
{
    [Property(DisplayName = "Iterates on some with unit")]
    public void OnSome(NonNull<string> a)
    {
        var witness = new Witness();

        Some(a.Get)
            .Iter(_ => witness.Touch());
        witness.ShouldHaveBeenTouched(1);

        None.Option<string>()
            .Iter(_ => witness.Touch());
        witness.ShouldHaveBeenTouched(1);
    }

    [Property(DisplayName = "Iterates on none with unit")]
    public void OnNone(NonNull<string> a)
    {
        var witness = new Witness();

        Some(a.Get)
            .Iter(() => witness.Touch());
        witness.ShouldHaveBeenTouched(0);

        None.Option<string>()
            .Iter(() => witness.Touch());
        witness.ShouldHaveBeenTouched(1);
    }

    [Property(DisplayName = "Iterates on some with both functions")]
    public void OnSomeBothFunctions(NonNull<string> a)
    {
        var witness = new Witness();
        Some(a.Get).Iter(_ => witness.Touch(), () => witness.Touch());
        witness.ShouldHaveBeenTouched(1);
    }

    [Property(DisplayName = "Iterates on none with both functions")]
    public void OnNoneBothFunctions(NonNull<string> a)
    {
        var witness = new Witness();
        None.Option<string>().Iter(_ => witness.Touch(), () => witness.Touch());
        witness.ShouldHaveBeenTouched(1);
    }
}
