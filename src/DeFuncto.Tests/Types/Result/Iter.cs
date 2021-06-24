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
            Ok("banana").ToResult<int>()
                .Iter(str =>
                {
                    witness += str.Length;
                })
                .Iter(str =>
                {
                    witness += str.Length;
                    return unit;
                })
                .Iter(str =>
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
            Error("banana").ToResult<int>()
                .Iter(str => { witness += str.Length; })
                .Iter(str =>
                {
                    witness += str.Length;
                    return unit;
                })
                .Iter(
                    _ => throw new Exception("Should not run"),
                    str =>
                    {
                        witness += str.Length;
                        return unit;
                    })
                .Iter(_ => throw new Exception("Should not run"), str => { witness += str.Length; });
            Assert.Equal(24, witness);
        }
    }
}
