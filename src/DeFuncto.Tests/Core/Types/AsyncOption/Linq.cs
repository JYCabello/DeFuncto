using System;
using DeFuncto.Assertions;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption;

public class Linq
{
    [Property(DisplayName = "Binds all somes")]
    public void AllSome(string x, string y, string z) => (
            from a in Some(x).Async()
            from b in Some(y).Async()
            from c in Some(z).Async()
            where c == y
            select a + b + c)
        .ShouldBeSome(x + y + z);

    [Property(DisplayName = "Stops at one none")]
    public void StopsNone(string x, string y) => (
            from a in Some(x).Async()
            from b in None.Option<string>().Async()
            let error = ThrowHelper()
            from c in Some(y).Async()
            where c == y
            select $"{a}{b}{c}")
        .ShouldBeNone();

    [Property(DisplayName = "Filters out")]
    public void FiltersOut(string w, string x, string y, string z) => (
            from a in Some(x).Async()
            from b in Some(y).Async()
            from c in Some(z).Async()
            where c == w
            select $"{a}{b}{c}")
        .ShouldBeNone();

    private static string ThrowHelper() => throw new Exception("Should not happen");
}
