using System;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.Result
{
    public class Linq
    {
        [Fact(DisplayName = "Carries along all values")]
        public void AllOk()
        {
            var result =
                from ok1 in Ok<string, int>("na")
                from ok2 in Ok<string, int>("na")
                from ok3 in Ok<string, int>("na")
                from ok4 in Ok<string, int>("na")
                from ok5 in Ok<string, int>("na")
                from ok6 in Ok<string, int>("na")
                from ok7 in Ok<string, int>("batman")
                select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!";

            Assert.True(result.IsOk);
            Assert.NotNull(result.OkValue);
            Assert.Equal("na na na na na na batman!", result.OkValue);
        }

        [Fact(DisplayName = "Gets the first error found")]
        public void GetsFirstError()
        {
            var result =
                from ok1 in Ok<string, int>("na")
                from ok2 in Ok<string, int>("na")
                from ok3 in Ok<string, int>("na")
                from ok4 in Error<string, int>(42)
                from ok5 in Ok<string, int>("na")
                from ok6 in Ok<string, int>("na")
                from ok7 in Ok<string, int>("batman")
                select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!";

            Assert.True(result.IsError);
            Assert.Equal(42, result.ErrorValue);
        }

        [Fact(DisplayName = "Stops running after first error")]
        public void ShortCircuit()
        {
            var result =
                from ok1 in Ok<string, int>("na")
                from ok2 in Ok<string, int>("na")
                from ok3 in Ok<string, int>("na")
                from ok4 in Error<string, int>(42)
                let boom = Boom()
                from ok5 in Ok<string, int>("na")
                from ok6 in Ok<string, int>("na")
                from ok7 in Ok<string, int>("batman")
                select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!";

            Assert.True(result.IsError);
            Assert.Equal(42, result.ErrorValue);

            int Boom() => throw new Exception("Should not happen");
        }
    }
}
