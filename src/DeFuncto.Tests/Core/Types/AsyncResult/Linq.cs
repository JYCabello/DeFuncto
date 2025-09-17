using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncResult;

public class Linq
{
    [Property(DisplayName = "Carries along all values")]
    public void AllOk(NonNull<string> a, NonNull<string> b, NonNull<string> c, NonNull<string> d, NonNull<string> e, NonNull<string> f) =>
        _ = (
            from ok1 in Ok<string, int>(a.Get).Async()
            from ok2 in Ok<string, int>(b.Get).Async()
            from ok3 in Ok<string, int>(c.Get).Async()
            from ok4 in Ok<string, int>(d.Get).Async()
            from ok5 in Ok<string, int>(e.Get).Async()
            from ok6 in Ok<string, int>(f.Get).Async()
            select $"{ok1}{ok2}{ok3}{ok4}{ok5}{ok6}"
        ).ShouldBeOk(a.Get + b.Get + c.Get + d.Get + e.Get + f.Get).Result;

    [Property(DisplayName = "Gets the first error found")]
    public void GetsFirstError(NonNull<string> a, NonNull<string> b, NonNull<string> c, NonNull<string> d, NonNull<string> e, NonNull<string> f, int g) =>
        _ = (
            from ok1 in Ok<string, int>(a.Get).Async()
            from ok2 in Ok<string, int>(b.Get).Async()
            from ok3 in Ok<string, int>(c.Get).Async()
            from ok4 in Error<string, int>(g).Async()
            from ok5 in Ok<string, int>(d.Get).Async()
            from ok6 in Ok<string, int>(e.Get).Async()
            from ok7 in Ok<string, int>(f.Get).Async()
            select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!"
        ).ShouldBeError(g).Result;

    [Property(DisplayName = "Stops running after first error")]
    public void ShortCircuit(NonNull<string> a, NonNull<string> b, NonNull<string> c, NonNull<string> d, NonNull<string> e, NonNull<string> f, int g)
    {
        _ = (
            from ok1 in Ok<string, int>(a.Get).Async()
            from ok2 in Ok<string, int>(b.Get).Async()
            from ok3 in Ok<string, int>(c.Get).Async()
            from ok4 in Error<string, int>(g).Async()
            let boom = Boom()
            from ok5 in Ok<string, int>(d.Get).Async()
            from ok6 in Ok<string, int>(e.Get).Async()
            from ok7 in Ok<string, int>(f.Get).Async()
            select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!"
        ).ShouldBeError(g).Result;

        int Boom() => throw new Exception("Should not happen");
    }

    [Property(DisplayName = "AsyncResult Select should project results")]
    public async void SelectProjectsResult()
    {
        // leave the type castings to ensure it's not returning a nested Result<Result...>>

        await ((AsyncResult<decimal, int>)(from _ in Ok<string, int>(string.Empty).Async() select Ok<decimal, int>(decimal.Zero).Async())).ShouldBeOk();
        await ((AsyncResult<string, int>)(from _ in Ok<string, int>(string.Empty).Async() select Ok<string, int>(string.Empty).Async())).ShouldBeOk();

        await ((AsyncResult<string, int>)(from _ in Ok<string, int>(string.Empty).Async() select Error<string, int>(1).Async())).ShouldBeError(1);
        await ((AsyncResult<string, int>)(from _ in Error<string, int>(1).Async() select Error<string, int>(2).Async())).ShouldBeError(1);
        await ((AsyncResult<string, int>)(from _ in Error<string, int>(1).Async() select Ok<string, int>(string.Empty).Async())).ShouldBeError(1);
    }

    [Property(DisplayName = "AsyncResult SelectMany should project results")]
    public async void SelectManyProjectsResult()
    {
        // leave the type castings to ensure it's not returning a nested Result<Result...>>

        await ((AsyncResult<string, int>)(
            from x in Ok<string, int>(string.Empty).Async()
            from y in Ok<string, int>(string.Empty).Async()
            from z in Ok<string, int>(string.Empty).Async()
            select Ok<string, int>("out").Async()))
            .ShouldBeOk("out");

        await ((AsyncResult<string, int>)(
            from x in Ok<string, int>(string.Empty).Async()
            from y in Ok<string, int>(string.Empty).Async()
            from z in Ok<string, int>(string.Empty).Async()
            select Error<string, int>(1).Async()))
            .ShouldBeError(1);

        await ((AsyncResult<string, int>)(
            from x in Ok<string, int>(string.Empty).Async()
            from y in Ok<string, int>(string.Empty).Async()
            from z in Error<string, int>(1).Async()
            select Error<string, int>(2).Async()))
            .ShouldBeError(1);

        await ((AsyncResult<decimal, int>)(
            from x in Ok<string, int>(string.Empty).Async()
            from y in Ok<string, int>(string.Empty).Async()
            from z in Error<string, int>(1).Async()
            select Error<decimal, int>(2).Async()))
            .ShouldBeError(1);
    }

    [Property(DisplayName = "AsyncResult is projected when using linq syntax for select over a projection of Task<Result<>> or Result<>")]
    public async void SelectAlwaysProjectsAsyncResult()
    {
        async Task<Result<string, int>> SomeAsyncMethod(string x) => Ok<string, int>($"{x}_some");

        // task
        AsyncResult<string, int> _1 = 
            from x in Ok<string, int>("ok").Async()
            select SomeAsyncMethod(x);

        (await _1).ShouldBeOk("ok_some");

        // result
        AsyncResult<string, int> _2 = 
            from x in Ok<string, int>("ok").Async()
            select Ok<string, int>($"{x}_sync");

        (await _2).ShouldBeOk("ok_sync");
    }

    [Property(DisplayName = "SelectMany should project Task binder and task projection.")]
    public void SelectManyTaskBinderTaskProjection()
    {
        async Task<Result<string, int>> SomeAsyncMethod(string x) => Ok<string, int>($"{x}_some");

        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            from y in SomeAsyncMethod(x)
            select SomeAsyncMethod(y);

        _ = result.ShouldBeOk("ok_some_some").Result;
    }

    [Property(DisplayName = "SelectMany should project Task binder and async projection.")]
    public void SelectManyTaskBinderAsyncProjection()
    {
        async Task<Result<string, int>> SomeAsyncMethod(string x) => Ok<string, int>($"{x}_some");

        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            from y in SomeAsyncMethod(x)
            select SomeAsyncMethod(y).Async();

        _ = result.ShouldBeOk("ok_some_some").Result;
    }

    [Property(DisplayName = "SelectMany should project Task binder and result projection")]
    public void SelectManyTaskBinderResultProjection()
    {
        async Task<Result<string, int>> SomeAsyncMethod(string x) => Ok<string, int>($"{x}_some");

        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            from y in SomeAsyncMethod(x)
            select Ok<string, int>($"{y}_sync");

        _ = result.ShouldBeOk("ok_some_sync").Result;
    }

    [Property(DisplayName = "SelectMany should project Async binder and task projection")]
    public void SelectManyAsyncBinderTaskProjection()
    {
        async Task<Result<string, int>> SomeAsyncMethod(string x) => Ok<string, int>($"{x}_some");

        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            from y in Ok<string, int>($"{x}_some").Async()
            select SomeAsyncMethod(y);

        _ = result.ShouldBeOk("ok_some_some").Result;
    }

    [Property(DisplayName = "SelectMany should project Async binder and async projection")]
    public void SelectManyAsyncBinderAsyncProjection()
    {
        async Task<Result<string, int>> SomeAsyncMethod(string x) => Ok<string, int>($"{x}_some");

        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            from y in Ok<string, int>($"{x}_some").Async()
            select SomeAsyncMethod(y).Async();

        _ = result.ShouldBeOk("ok_some_some").Result;
    }

    [Property(DisplayName = "SelectMany should project Async binder and result projection")]
    public void SelectManyAsyncBinderResultProjection()
    {
        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            from y in Ok<string, int>($"{x}_some").Async()
            select Ok<string, int>($"{y}_sync");
        
        _ = result.ShouldBeOk("ok_some_sync").Result;
    }

    [Property(DisplayName = "SelectMany should project Result binder and task projection")]
    public void SelectManyResultBinderTaskProjection()
    {
        async Task<Result<string, int>> SomeAsyncMethod(string x) => Ok<string, int>($"{x}_some");

        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            from y in Ok<string, int>($"{x}_sync")
            select SomeAsyncMethod(y);

        _ = result.ShouldBeOk("ok_sync_some").Result;
    }

    [Property(DisplayName = "SelectMany should project Result binder and async projection")]
    public void SelectManyResultBinderAsyncProjection()
    {
        async Task<Result<string, int>> SomeAsyncMethod(string x) => Ok<string, int>($"{x}_some");

        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            from y in Ok<string, int>($"{x}_sync")
            select SomeAsyncMethod(y).Async();

        _ = result.ShouldBeOk("ok_sync_some").Result;
    }

    [Property(DisplayName = "SelectMany should project Result binder and result projection")]
    public void SelectManyResultBinderResultProjection()
    {
        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            from y in Ok<string, int>($"{x}_sync")
            select Ok<string, int>($"{y}_sync");

        _ = result.ShouldBeOk("ok_sync_sync").Result;
    }

    [Property(DisplayName = "SelectMany should project task<result>")]
    public void SelectWithTaskProjection()
    {
        async Task<Result<string, int>> SomeAsyncMethod(string x) => Ok<string, int>($"{x}_some");
        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            select SomeAsyncMethod(x);
        _ = result.ShouldBeOk("ok_some").Result;
    }

    [Property(DisplayName = "SelectMany should project result")]
    public void SelectWithResultProjection()
    {
        Result<string, int> SomeMethod(string x) => Ok<string, int>($"{x}_some");
        AsyncResult<string, int> result =
            from x in Ok<string, int>("ok").Async()
            select SomeMethod(x);
        _ = result.ShouldBeOk("ok_some").Result;
    }
}
