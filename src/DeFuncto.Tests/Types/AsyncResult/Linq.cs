using System;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class Linq
    {
        [Fact(DisplayName = "Carries along all values")]
        public void AllOk() =>
        (
            from ok1 in Ok<string, int>("b").Async()
            from ok2 in Ok<string, int>("a").Async()
            from ok3 in Ok<string, int>("n").Async()
            from ok4 in Ok<string, int>("a").Async()
            from ok5 in Ok<string, int>("n").Async()
            from ok6 in Ok<string, int>("a").Async()
            select $"{ok1}{ok2}{ok3}{ok4}{ok5}{ok6}"
        ).ShouldBeOk("banana");

        [Fact(DisplayName = "Gets the first error found")]
        public void GetsFirstError() =>
        (
            from ok1 in Ok<string, int>("na").Async()
            from ok2 in Ok<string, int>("na").Async()
            from ok3 in Ok<string, int>("na").Async()
            from ok4 in Error<string, int>(42).Async()
            from ok5 in Ok<string, int>("na").Async()
            from ok6 in Ok<string, int>("na").Async()
            from ok7 in Ok<string, int>("batman").Async()
            select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!"
        ).ShouldBeError(42);

        [Fact(DisplayName = "Stops running after first error")]
        public void ShortCircuit()
        {
            (
                from ok1 in Ok<string, int>("na").Async()
                from ok2 in Ok<string, int>("na").Async()
                from ok3 in Ok<string, int>("na").Async()
                from ok4 in Error<string, int>(42).Async()
                let boom = Boom()
                from ok5 in Ok<string, int>("na").Async()
                from ok6 in Ok<string, int>("na").Async()
                from ok7 in Ok<string, int>("batman").Async()
                select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!"
            ).ShouldBeError(42);

            int Boom() => throw new Exception("Should not happen");
        }
    }
}
