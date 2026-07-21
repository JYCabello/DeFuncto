using System;
using System.Threading.Tasks;
using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;

namespace DeFuncto.Tests.Core.Types.Result;

public class Linq
{
    [Property(DisplayName = "Carries along all values")]
    public void AllOk(
        NonNull<string> a,
        NonNull<string> b,
        NonNull<string> c,
        NonNull<string> d,
        NonNull<string> e,
        NonNull<string> f) =>
    (
        from ok1 in Ok<string, int>(a.Get)
        from ok2 in Ok<string, int>(b.Get)
        from ok3 in Ok<string, int>(c.Get)
        from ok4 in Ok<string, int>(d.Get)
        from ok5 in Ok<string, int>(e.Get)
        from ok6 in Ok<string, int>(f.Get)
        select $"{ok1}{ok2}{ok3}{ok4}{ok5}{ok6}"
    ).ShouldBeOk($"{a.Get}{b.Get}{c.Get}{d.Get}{e.Get}{f.Get}");

    [Property(DisplayName = "Gets the first error found")]
    public void GetsFirstError(
        NonNull<string> a,
        NonNull<string> b,
        NonNull<string> c,
        NonNull<string> d,
        NonNull<string> e,
        NonNull<string> f,
        int g) =>
    (
        from ok1 in Ok<string, int>(a.Get)
        from ok2 in Ok<string, int>(b.Get)
        from ok3 in Ok<string, int>(c.Get)
        from ok4 in Error<string, int>(g)
        from ok5 in Ok<string, int>(d.Get)
        from ok6 in Ok<string, int>(e.Get)
        from ok7 in Ok<string, int>(f.Get)
        select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!"
    ).ShouldBeError(g);

    [Property(DisplayName = "Stops running after first error")]
    public void ShortCircuit(
        NonNull<string> a,
        NonNull<string> b,
        NonNull<string> c,
        NonNull<string> d,
        NonNull<string> e,
        NonNull<string> f,
        int g)
    {
        (
            from ok1 in Ok<string, int>(a.Get)
            from ok2 in Ok<string, int>(b.Get)
            from ok3 in Ok<string, int>(c.Get)
            from ok4 in Error<string, int>(g)
            let boom = Boom()
            from ok5 in Ok<string, int>(d.Get)
            from ok6 in Ok<string, int>(e.Get)
            from ok7 in Ok<string, int>(f.Get)
            select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!"
        ).ShouldBeError(g);

        int Boom() => throw new Exception("Should not happen");
    }

    [Property(DisplayName = "Result Select should project results")]
    public void SelectProjectsResult()
    {
        // leave the type castings to ensure it's not returning a nested Result<Result...>>
        
        ((Result<decimal, int>) (from _ in Ok<string, int>(string.Empty) select Ok<decimal,int>(decimal.Zero))).ShouldBeOk();
        ((Result<string, int>) (from _ in Ok<string, int>(string.Empty) select Ok<string, int>(string.Empty))).ShouldBeOk();
        
        ((Result<string,int>) (from _ in Ok<string, int>(string.Empty) select Error<string, int>(1))).ShouldBeError(1);
        ((Result<string,int>) (from _ in Error<string, int>(1) select Error<string, int>(2))).ShouldBeError(1);
        ((Result<string,int>) (from _ in Error<string, int>(1) select Ok<string, int>(string.Empty))).ShouldBeError(1);
    }

    [Property(DisplayName = "Result SelectMany should project results")]
    public void SelectManyProjectsResult()
    {
        // leave the type castings to ensure it's not returning a nested Result<Result...>>

        ((Result<string, int>)(
            from x in Ok<string, int>(string.Empty)
            from y in Ok<string, int>(string.Empty)
            from z in Ok<string, int>(string.Empty)
            select Ok<string, int>("out")))
            .ShouldBeOk("out");

        ((Result<string, int>)(
            from x in Ok<string, int>(string.Empty)
            from y in Ok<string, int>(string.Empty)
            from z in Ok<string, int>(string.Empty)
            select Error<string, int>(1)))
            .ShouldBeError(1);

        ((Result<string, int>)(
            from x in Ok<string, int>(string.Empty)
            from y in Ok<string, int>(string.Empty)
            from z in Error<string, int>(1)
            select Error<string, int>(2)))
            .ShouldBeError(1);

        ((Result<decimal, int>)(
            from x in Ok<string, int>(string.Empty)
            from y in Ok<string, int>(string.Empty)
            from z in Error<string, int>(1)
            select Error<decimal, int>(2)))
            .ShouldBeError(1);
    }

    [Property(DisplayName = "Select should project async Task<Result>")]
    public void SelectTaskProjection()
    {
        Task<Result<string, int>> Foo(string r) => Task.FromResult(Ok<string, int>("foo"));
        var x = from r in Ok<string, int>(string.Empty)
                select Foo(r);

        x.ToTask().Result.ShouldBeOk("foo");
    }

    [Property(DisplayName = "Select should project AsyncResult")]
    public void SelectAsyncProjection()
    {
        AsyncResult<string, int> Foo(string r) => Task.FromResult(Ok<string, int>("foo")).Async();
        var x = from r in Ok<string, int>(string.Empty)
                select Foo(r);

        x.ToTask().Result.ShouldBeOk("foo");
    }

    [Property(DisplayName = "SelectMany should bind and project AsyncResult")]
    public void SelectManyAsyncBinderAsyncProjection()
    {
        AsyncResult<string, int> Bar(string foo) => Task.FromResult(Ok<string, int>($"{foo}bar")).Async();
        AsyncResult<string,int> x = from r in Ok<string, int>("foo")
                from s in Bar(r)
                select Bar(s);

        x.ToTask().Result.ShouldBeOk("foobarbar");
    }

    [Property(DisplayName = "Select should bind Task<Result> and project AsyncResult")]
    public void SelectManyTaskBinderAsyncProjection()
    {
        Task<Result<string, int>> BarTask(string foo) => Task.FromResult(Ok<string, int>($"{foo}bar"));
        AsyncResult<string, int> BarAsync(string foo) => Task.FromResult(Ok<string, int>($"{foo}bas")).Async();

        AsyncResult<string, int> x = from r in Ok<string, int>("foo")
                                     from s in BarTask(r)
                                     select BarAsync(s);

        x.ToTask().Result.ShouldBeOk("foobarbas");
    }

    [Property(DisplayName = "Select should bind AsyncResult and project Task<Result>")]
    public void SelectManyAsyncBinderTaskProjection()
    {
        Task<Result<string, int>> BarTask(string foo) => Task.FromResult(Ok<string, int>($"{foo}bas"));
        AsyncResult<string, int> BarAsync(string foo) => Task.FromResult(Ok<string, int>($"{foo}bar")).Async();

        AsyncResult<string, int> x = from r in Ok<string, int>("foo")
                                     from s in BarAsync(r)
                                     select BarTask(s);

        x.ToTask().Result.ShouldBeOk("foobarbas");
    }

    [Property(DisplayName = "Select should bind and project Task<Result>")]
    public void SelectManyTaskBinderTaskProjection()
    {
        Task<Result<string, int>> BarTask(string foo) => Task.FromResult(Ok<string, int>($"{foo}bas"));

        AsyncResult<string, int> x = from r in Ok<string, int>("foo")
                                     from s in BarTask(r)
                                     select BarTask(s);

        x.ToTask().Result.ShouldBeOk("foobasbas");
    }
}
