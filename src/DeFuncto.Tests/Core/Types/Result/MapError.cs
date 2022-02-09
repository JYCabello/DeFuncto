using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result;

public class MapError
{
    [Property(DisplayName = "Maps the error")]
    public void MapsError(NonNull<string> a, NonNull<string> b) =>
        Error<int, string>(a.Get)
            .MapError(str => $"{str}{b.Get}")
            .ShouldBeError($"{a.Get}{b.Get}");
}
