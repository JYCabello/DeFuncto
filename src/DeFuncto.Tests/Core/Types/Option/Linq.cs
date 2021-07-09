using System;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Linq
    {
        [Fact(DisplayName = "Binds all somes")]
        public void AllSome() => (
                from a in Some("ba")
                from b in Some("na")
                from c in Some("na")
                where c == "na"
                select $"{a}{b}{c}")
            .ShouldBeSome("banana");

        [Fact(DisplayName = "Stops at one none")]
        public void StopsNone() => (
                from a in Some("ba")
                from b in None.Option<string>()
                let error = Boom()
                from c in Some("na")
                where c == "na"
                select $"{a}{b}{c}")
            .ShouldBeNone();

        [Fact(DisplayName = "Filters out")]
        public void FiltersOut() => (
                from a in Some("ba")
                from b in Some("na")
                from c in Some("na")
                where c == "pear"
                select $"{a}{b}{c}")
            .ShouldBeNone();

        private string Boom() => throw new Exception("Should not happen");
    }
}
