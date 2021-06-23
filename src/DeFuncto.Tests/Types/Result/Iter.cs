using System;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.Result
{
    public class Iter
    {
        [Fact(DisplayName = "Iterates on Ok")]
        public void OnOk()
        {
            var witness = 0;
            var ok = Ok("banana").ToResult<int>();
            ok.Iter(str =>
            {
                witness += str.Length;
            });
            ok.Iter(str =>
            {
                witness += str.Length;
                return unit;
            });
            ok.Iter(str =>
                {
                    witness += str.Length;
                    return unit;
                },
                _ => throw new Exception("Should not run"));
            Assert.Equal(18, witness);
        }

        [Fact(DisplayName = "Iterates on Error")]
        public void OnError()
        {
            var witness = 0;
            var error = Error("banana").ToResult<int>();
            error.Iter(str => { witness += str.Length; });
            error.Iter(str =>
            {
                witness += str.Length;
                return unit;
            });
            error.Iter(
                _ => throw new Exception("Should not run"),
                str =>
                {
                    witness += str.Length;
                    return unit;
                });
            error.Iter(_ => throw new Exception("Should not run"), str => { witness += str.Length; });
            Assert.Equal(24, witness);
        }
    }
}
