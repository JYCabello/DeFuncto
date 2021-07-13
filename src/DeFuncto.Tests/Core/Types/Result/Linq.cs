using System;
using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Result
{
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
    }
}
