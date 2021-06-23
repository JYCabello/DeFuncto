using System;
using Xunit;

namespace DeFuncto.Tests.Types.Result
{
    public class Linq
    {
        [Fact(DisplayName = "Carries along all values")]
        public void AllOk()
        {
            var result =
                from ok1 in Result<string, int>.Ok("na")
                from ok2 in Result<string, int>.Ok("na")
                from ok3 in Result<string, int>.Ok("na")
                from ok4 in Result<string, int>.Ok("na")
                from ok5 in Result<string, int>.Ok("na")
                from ok6 in Result<string, int>.Ok("na")
                from ok7 in Result<string, int>.Ok("batman")
                select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!";

            Assert.True(result.IsOk);
            Assert.NotNull(result.OkValue);
            Assert.Equal("na na na na na na batman!", result.OkValue);
        }

        [Fact(DisplayName = "Gets the first error found")]
        public void GetsFirstError()
        {
            var result =
                from ok1 in Result<string, int>.Ok("na")
                from ok2 in Result<string, int>.Ok("na")
                from ok3 in Result<string, int>.Ok("na")
                from ok4 in Result<string, int>.Error(42)
                from ok5 in Result<string, int>.Ok("na")
                from ok6 in Result<string, int>.Ok("na")
                from ok7 in Result<string, int>.Ok("batman")
                select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!";

            Assert.True(result.IsError);
            Assert.Equal(42, result.ErrorValue);
        }

        [Fact(DisplayName = "Stops running after first error")]
        public void ShortCircuit()
        {
            var result =
                from ok1 in Result<string, int>.Ok("na")
                from ok2 in Result<string, int>.Ok("na")
                from ok3 in Result<string, int>.Ok("na")
                from ok4 in Result<string, int>.Error(42)
                let boom = Boom()
                from ok5 in Result<string, int>.Ok("na")
                from ok6 in Result<string, int>.Ok("na")
                from ok7 in Result<string, int>.Ok("batman")
                select $"{ok1} {ok2} {ok3} {ok4} {ok5} {ok6} {ok7}!";

            Assert.True(result.IsError);
            Assert.Equal(42, result.ErrorValue);

            int Boom() => throw new Exception("Should not happen");
        }
    }
}
