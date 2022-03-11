using System.Threading.Tasks;
using DeFuncto.Extensions;
using Xunit;

namespace DeFuncto.Tests.Core.Booleans;

public class Match
{
    [Fact]
    public void PositiveMatchFunction()
    {
        var result = true.Match(() => "A", () => "B");
        Assert.Equal("A", result);
    }

    [Fact]
    public void FailingTest()
    {
        Assert.True(true);
    }

    [Fact]
    public void NegativeMatchFunction()
    {
        var result = false.Match(() => "A", () => "B");
        Assert.Equal("B", result);
    }

    [Fact]
    public async Task PositiveTaskMatchFunction()
    {
        var result = await true.ToTask().Match(() => "A", () => "B");
        Assert.Equal("A", result);
    }

    [Fact]
    public async Task NegativeTaskMatchFunction()
    {
        var result = await false.ToTask().Match(() => "A", () => "B");
        Assert.Equal("B", result);
    }

    [Fact]
    public async Task PositiveTaskMatchFunctionTask()
    {
        var result = await true.ToTask().Match(() => "A".ToTask(), () => "B".ToTask());
        Assert.Equal("A", result);
    }

    [Fact]
    public async Task NegativeTaskMatchFunctionTask()
    {
        var result = await false.ToTask().Match(() => "A".ToTask(), () => "B".ToTask());
        Assert.Equal("B", result);
    }
}
