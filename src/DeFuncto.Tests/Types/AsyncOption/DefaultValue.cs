using System.Threading.Tasks;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncOption
{
    public class DefaultValue
    {
        [Fact(DisplayName = "Gets the default value on None")]
        public Task DefaultsOnNone() =>
            None.Option<string>()
                .Async()
                .DefaultValue("banana")
                .Run(val => Assert.Equal("banana", val));

        [Fact(DisplayName = "Gets the default value on None with a task")]
        public Task DefaultsOnNoneTask() =>
            None.Option<string>()
                .Async()
                .DefaultValue("banana".ToTask())
                .Run(val => Assert.Equal("banana", val));

        [Fact(DisplayName = "Skips the default value on Some")]
        public Task SkipsOnSome() =>
            Some("banana")
                .Async()
                .DefaultValue("pear")
                .Run(val => Assert.Equal("banana", val));

        [Fact(DisplayName = "Skips the default value on Some with a task")]
        public Task SkipsOnSomeTask() =>
            Some("banana")
                .Async()
                .DefaultValue("pear".ToTask())
                .Run(val => Assert.Equal("banana", val));
    }
}
