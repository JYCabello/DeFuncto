using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option
{
    public class Bind
    {
        [Property(DisplayName = "Binds two somes")]
        public void SomeOnSome(NonNull<string> a, NonNull<string> b) =>
            Some(42)
                .Bind(number => number == 42 ? Some(a.Get) : Some(b.Get))
                .ShouldBeSome(a.Get);

        [Property(DisplayName = "Binds none after some")]
        public void NoneOnSome(NonNull<string> a) =>
            Some(a.Get)
                .Bind(_ => None.Option<int>())
                .ShouldBeNone();

        [Fact(DisplayName = "Skips none after none")]
        public void NoneOnNone() =>
            None.Option<string>()
                .Bind(_ => None.Option<int>())
                .ShouldBeNone();

        [Property(DisplayName = "Skip some after none")]
        public void SomeOnNone(NonNull<string> a, NonNull<string> b) =>
            None.Option<int>()
                .Bind(number => number == 42 ? Some(a.Get) : Some(b.Get))
                .ShouldBeNone();
    }
}
