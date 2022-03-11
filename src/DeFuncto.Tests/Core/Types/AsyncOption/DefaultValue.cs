using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption;

public class DefaultValue
{
    [Property(DisplayName = "Gets the default value on None")]
    public void DefaultsOnNone(string a) =>
        None.Option<string>()
            .Async()
            .DefaultValue(a)
            .Result
            .Run(val => Assert.Equal(a, val));

    [Property(DisplayName = "Gets the default value on None with a task")]
    public void DefaultsOnNoneTask(string a) =>
        None.Option<string>()
            .Async()
            .DefaultValue(a.ToTask())
            .Result
            .Run(val => Assert.Equal(a, val));

    [Property(DisplayName = "Skips the default value on Some")]
    public void SkipsOnSome(NonNull<string> a, NonNull<string> b) =>
        Some(a)
            .Async()
            .DefaultValue(b)
            .Result
            .Run(val => Assert.Equal(a, val));

    [Property(DisplayName = "Skips the default value on Some with a task")]
    public void SkipsOnSomeTask(string a, string b) =>
        Some(a)
            .Async()
            .DefaultValue(b.ToTask())
            .Result
            .Run(val => Assert.Equal(a, val));
}
