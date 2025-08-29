using System;
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
}
