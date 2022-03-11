using System;
using System.Threading.Tasks;
using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Tries;

public class Try
{
    [Property]
    public void TryOk(int a) =>
        Try(() => a)
            .ShouldBeOk(a);

    [Property]
    public void TryError(NonNull<string> a)
    {
        var expected = new Exception(a.Get);
        Try(() => ThrowHelper(expected))
            .ShouldBeError(expected);
    }

    [Fact]
    public async Task TryAsyncOk()
    {
        var expected = Guid.NewGuid().ToString();
        await Try(() => Task.FromResult(expected))
            .ShouldBeOk(expected);
    }

    [Fact]
    public async Task TryAsyncError()
    {
        var expected = new Exception(Guid.NewGuid().ToString());
        await Try(() => ThrowHelperAsync(expected))
            .ShouldBeError(expected);
    }

    private int ThrowHelper(Exception ex) =>
        throw ex;

    private Task<int> ThrowHelperAsync(Exception ex) =>
        throw ex;
}
