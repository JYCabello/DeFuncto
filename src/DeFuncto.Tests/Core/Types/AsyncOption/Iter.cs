using System;
using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption;

public class Iter
{
    [Fact(DisplayName = "Iterates on Some synchronously with both iterators")]
    public async Task SomeSync()
    {
        var some = Some(Guid.NewGuid().ToString()).Async();
        var witness = new Witness();
        await some.Iter(
            _ => { witness.Touch(); },
            () => { witness.Touch(); }
        );
        await some.Iter(() => witness.Touch());
        await some.Iter(_ => witness.Touch());
        witness.ShouldHaveBeenTouched(2);
    }

    [Fact(DisplayName = "Iterates on None synchronously with both iterators")]
    public async Task NoneSync()
    {
        var some = Some(Guid.NewGuid().ToString()).Async();
        var witness = new Witness();
        await some.Iter(
            _ => { witness.Touch(); },
            () => { witness.Touch(); }
        );
        witness.ShouldHaveBeenTouched(1);
    }

    [Fact(DisplayName = "Iterates on Some asynchronously with both iterators")]
    public async Task SomeAsyncDouble()
    {
        var some = Some(Guid.NewGuid().ToString()).Async();
        var witness = new Witness();
        await some.Iter(
            _ => witness.Touch().ToTask(),
            () => witness.Touch().ToTask()
        );
        witness.ShouldHaveBeenTouched(1);
    }

    [Fact(DisplayName = "Iterates on None asynchronously")]
    public async Task NoneAsync()
    {
        var none = None.Option<string>().Async();
        var witness = new Witness();
        await none.Iter(() => witness.Touch().ToTask());
        await none.Iter(_ => witness.Touch().ToTask());
        witness.ShouldHaveBeenTouched(1);
    }

    [Property(DisplayName = "Iterates on Some asynchronously")]
    public void SomeAsync(NonNull<string> val)
    {
        var some = Some(val).Async();
        var witness = new Witness();
        some.Iter(() => witness.Touch().ToTask()).Wait();
        some.Iter(_ => witness.Touch().ToTask()).Wait();
        witness.ShouldHaveBeenTouched(1);
    }
}
