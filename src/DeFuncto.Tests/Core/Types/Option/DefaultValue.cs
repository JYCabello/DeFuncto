using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class DefaultValue
    {
        [Fact(DisplayName = "If none, gets default value")]
        public void OnNone() =>
            None.Option<string>()
                .DefaultValue("banana")
                .Run(v => Assert.Equal("banana", v));

        [Fact(DisplayName = "If some, gets existing value")]
        public void OnSom() =>
            Some("banana")
                .DefaultValue("pear")
                .Run(v => Assert.Equal("banana", v));
    }
}
