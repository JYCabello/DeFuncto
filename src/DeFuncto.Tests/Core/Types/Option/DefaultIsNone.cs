using DeFuncto.Assertions;
using Xunit;

namespace DeFuncto.Tests.Core.Types.Option
{
    // An option is the only structure in this library that is actually 100% safe, as if it were to default
    // for whatever reason, it would treat itself as `None`, leaving it always in a valid state.
    public class DefaultIsNone
    {
        [Fact(DisplayName = "Defaults to none")]
        public void Defaults()
        {
            Option<string> option = default;
            option.ShouldBeNone();
        }
    }
}
