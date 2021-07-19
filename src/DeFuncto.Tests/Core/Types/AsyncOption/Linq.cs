using System;
using DeFuncto.Assertions;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class Linq
    {
        [Property(DisplayName = "Binds all somes")]
        public void AllSome(string x, string y, string z) => (
                from a in Some(x).Async()
                from b in Some(y).Async()
                from c in Some(z).Async()
                where c == y
                select (a + b + c))
            .ShouldBeSome(x + y + z);

        [Fact(DisplayName = "Stops at one none")]
        public void StopsNone() => (
                from a in Some("ba").Async()
                from b in None.Option<string>().Async()
                let error = Boom()
                from c in Some("na").Async()
                where c == "na"
                select $"{a}{b}{c}")
            .ShouldBeNone();

        [Fact(DisplayName = "Filters out")]
        public void FiltersOut() => (
                from a in Some("ba").Async()
                from b in Some("na").Async()
                from c in Some("na").Async()
                where c == "pear"
                select $"{a}{b}{c}")
            .ShouldBeNone();

        private string Boom() => throw new Exception("Should not happen");
    }
}
