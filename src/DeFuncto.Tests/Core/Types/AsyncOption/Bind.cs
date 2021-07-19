using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class Bind
    {
        [Property(DisplayName = "Binds two somes")]
        public void SomeOnSome(string a, string b) =>
            Some(42)
                .Async()
                .Bind(number => number == 42 ? Some(a) : Some(b))
                .ShouldBeSome(a);

        [Property(DisplayName = "Binds none after some")]
        public void NoneOnSome(string a) =>
            Some(a)
                .Async()
                .Bind(_ => None.Option<int>().ToTask())
                .ShouldBeNone();

        [Fact(DisplayName = "Skips none after none")]
        public void NoneOnNone() =>
            None.Option<string>()
                .Async()
                .Bind(_ => None.Option<int>())
                .ShouldBeNone();

        [Fact(DisplayName = "Skips some after none")]
        public void SomeOnNone() =>
            None.Option<int>()
                .Async()
                .Bind(number => number == 42 ? Some("banana") : Some("pear"))
                .ShouldBeNone();
    }
}
