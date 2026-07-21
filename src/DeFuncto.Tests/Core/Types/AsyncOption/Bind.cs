using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.AsyncOption;

public class Bind
{
    [Property(DisplayName = "Binds two somes")]
    public void SomeOnSome(NonNull<string> a, NonNull<string> b) =>
        Some(42)
            .Async()
            .Bind(number => number == 42 ? Some(a.Get) : Some(b.Get))
            .ShouldBeSome(a.Get)
            .GetAwaiter()
            .GetResult();

    [Property(DisplayName = "Binds none after some")]
    public void NoneOnSome(string a) =>
        Some(a)
            .Async()
            .Bind(_ => None.Option<int>().ToTask())
            .ShouldBeNone()
            .GetAwaiter()
            .GetResult();

    [Fact(DisplayName = "Skips none after none")]
    public async Task NoneOnNone() =>
        await None.Option<string>()
            .Async()
            .Bind(_ => None.Option<int>())
            .ShouldBeNone();

    [Property(DisplayName = "Skips some after none")]
    public void SomeOnNone(string a, string b) =>
        None.Option<int>()
            .Async()
            .Bind(number => number == 42 ? Some(a) : Some(b))
            .ShouldBeNone()
            .GetAwaiter()
            .GetResult();
}
