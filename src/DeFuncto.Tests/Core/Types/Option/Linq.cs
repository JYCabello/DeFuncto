using System;
using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option;

public class Linq
{
    [Property(DisplayName = "Binds all somes")]
    public void AllSome(NonNull<string> x, NonNull<string> y, NonNull<string> z) => (
            from a in Some(x.Get)
            from b in Some(y.Get)
            from c in Some(z.Get)
            where c == z.Get
            select a + b + c)
        .ShouldBeSome(x.Get + y.Get + z.Get);

    [Property(DisplayName = "Stops at one none")]
    public void StopsNone(NonNull<string> x, NonNull<string> y, NonNull<string> z) => (
            from a in Some(x.Get)
            from b in None.Option<string>()
            let error = Boom()
            from c in Some(y.Get)
            where c == z.Get
            select a + b + c)
        .ShouldBeNone();

    [Property(DisplayName = "Filters out")]
    public void FiltersOut(NonNull<string> x, NonNull<string> y, NonNull<string> z) => (
            from a in Some(x.Get)
            from b in Some(y.Get)
            from c in Some(z.Get)
            where c == Guid.NewGuid().ToString()
            select a + b + c)
        .ShouldBeNone();

    private static string Boom() => throw new Exception("Should not happen");
}
