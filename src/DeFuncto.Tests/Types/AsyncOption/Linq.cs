using System;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncOption
{
    public class Linq
    {
        [Fact(DisplayName = "Binds all somes")]
        public void AllSome() => (
                from a in Some("ba").Async()
                from b in Some("na").Async()
                from c in Some("na").Async()
                where c == "na"
                select $"{a}{b}{c}")
            .ShouldBeSome("banana");

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
