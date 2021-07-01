using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.Option
{
    public class Map
    {
        [Fact(DisplayName = "Maps Some")]
        public void OnSome() =>
            Some("ban")
                .Map(val => $"{val}ana")
                .ShouldBeSome("banana");

        [Fact(DisplayName = "Skips mapping on None")]
        public void OnNone() =>
            None
                .Option<string>()
                .Map(val => $"{val}ana")
                .ShouldBeNone();
    }
}
