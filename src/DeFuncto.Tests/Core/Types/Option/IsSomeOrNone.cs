using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option;

public class IsSomeOrNone
{
    [Property(DisplayName = "Evaluates Some")]
    public void IsSome(NonNull<string> a) =>
        Some(a.Get)
            .IsSome
            .Run(Assert.True);

    [Fact(DisplayName = "Evaluates None")]
    public void IsNone() =>
        None
            .Option<string>()
            .IsNone
            .Run(Assert.True);
}
