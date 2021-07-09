using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class IsSomeOrNone
    {
        [Fact(DisplayName = "Evaluates Some")]
        public void IsSome() =>
            Some("banana")
                .IsSome
                .Run(Assert.True);

        [Fact(DisplayName = "Evaluates None")]
        public void IsNone() =>
            None
                .Option<string>()
                .IsNone
                .Run(Assert.True);
    }
}
