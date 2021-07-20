using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class DefaultValue
    {
        [Property(DisplayName = "If none, gets default value")]
        public void OnNone(NonNull<string> a) =>
            None.Option<string>()
                .DefaultValue(a.Get)
                .Run(v => Assert.Equal(a.Get, v));

        [Property(DisplayName = "If some, gets existing value")]
        public void OnSom(NonNull<string> a, NonNull<string> b) =>
            Some(a.Get)
                .DefaultValue(b.Get)
                .Run(v => Assert.Equal(a.Get, v));
    }
}
