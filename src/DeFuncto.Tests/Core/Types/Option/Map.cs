using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option;

public class Map
{
    [Property(DisplayName = "Maps Some")]
    public void OnSome(NonNull<string> a, NonNull<string> b) =>
        Some(a.Get)
            .Map(val => val + b.Get)
            .ShouldBeSome(a.Get + b.Get);

    [Property(DisplayName = "Skips mapping on None")]
    public void OnNone(NonNull<string> a) =>
        None
            .Option<string>()
            .Map(val => val + a.Get)
            .ShouldBeNone();
}
