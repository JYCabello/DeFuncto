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
    public void SelectProjectsResult()
    {
        // leave the type castings to ensure it's not returning a nested Result<Result...>>

        ((AsyncResult<decimal, int>)(from _ in Ok<string, int>(string.Empty).Async() select Ok<decimal, int>(decimal.Zero).Async())).ShouldBeOk();
        ((AsyncResult<string, int>)(from _ in Ok<string, int>(string.Empty).Async() select Ok<string, int>(string.Empty).Async())).ShouldBeOk();

        ((AsyncResult<string, int>)(from _ in Ok<string, int>(string.Empty).Async() select Error<string, int>(1).Async())).ShouldBeError(1);
        ((AsyncResult<string, int>)(from _ in Error<string, int>(1).Async() select Error<string, int>(2).Async())).ShouldBeError(1);
        ((AsyncResult<string, int>)(from _ in Error<string, int>(1).Async() select Ok<string, int>(string.Empty).Async())).ShouldBeError(1);
    }

    [Property(DisplayName = "AsyncResult SelectMany should project results")]
    public void SelectManyProjectsResult()
    {
        // leave the type castings to ensure it's not returning a nested Result<Result...>>

        ((AsyncResult<string, int>)(
            from x in Ok<string, int>(string.Empty).Async()
            from y in Ok<string, int>(string.Empty).Async()
            from z in Ok<string, int>(string.Empty).Async()
            select Ok<string, int>("out").Async()))
            .ShouldBeOk("out");

        ((AsyncResult<string, int>)(
            from x in Ok<string, int>(string.Empty).Async()
            from y in Ok<string, int>(string.Empty).Async()
            from z in Ok<string, int>(string.Empty).Async()
            select Error<string, int>(1).Async()))
            .ShouldBeError(1);

        ((AsyncResult<string, int>)(
            from x in Ok<string, int>(string.Empty).Async()
            from y in Ok<string, int>(string.Empty).Async()
            from z in Error<string, int>(1).Async()
            select Error<string, int>(2).Async()))
            .ShouldBeError(1);

        ((AsyncResult<decimal, int>)(
            from x in Ok<string, int>(string.Empty).Async()
            from y in Ok<string, int>(string.Empty).Async()
            from z in Error<string, int>(1).Async()
            select Error<decimal, int>(2).Async()))
            .ShouldBeError(1);
    }

    [Property(DisplayName = "AsyncResult is projected when using linq syntax for select many over a Task<Result<>> or a Result<> method")]
    public async void SelectManyAlwaysProjectsAsyncResult()
    {
        /*
         
         Tests the following combinations for selectMany;
         
              #  binder	projection
             ----------------------
              1  task	task
              2  task	async
              3  task	result
              4  async	task
              5  async	async
              6  async	result
              7  result	task
              8  result	async
              9  result	result
        
         */

        async Task<Result<string, int>> SomeAsyncMethod(string x) => Ok<string, int>($"{x}_some");

        AsyncResult<string, int> _1 =
            from x in Ok<string, int>("ok").Async()
            from y in SomeAsyncMethod(x)
            select SomeAsyncMethod(y);

        (await _1).ShouldBeOk("ok_some_some");

    }

}
