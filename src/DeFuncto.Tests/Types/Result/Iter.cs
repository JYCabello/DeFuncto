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
            Ok("banana").Result<int>()
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
            Error("banana").Result<int>()
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

        [Fact(DisplayName = "Skips Error iter on Ok")]
        public void OnOkSkipError()
        {
            var witness = 0;
            Ok("banana").Result<int>()
                .Iter(str => { witness += str.Length; })
                .Iter(str =>
                {
                    witness += str.Length;
                    return unit;
                })
                .Iter(
                    str =>
                    {
                        witness += str.Length;
                        return unit;
                    },
                    _ => throw new Exception("Should not run"))
                .Iter(str => { witness += str.Length; }, _ => throw new Exception("Should not run"));
            Assert.Equal(24, witness);
        }
    }
}
