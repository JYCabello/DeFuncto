using FsCheck.Xunit;
using DeFuncto.Assertions;
using Xunit;
using System;
using FsCheck;

namespace DeFuncto.Tests.Core.Tries
{
    public class Try
    {
        [Property]
        public void TryOk(int a) =>
            DeFuncto.Extensions.Tests.Try(() => a)
                .ShouldBeOk(a);

        [Property]
        public void TryError(NonNull<string> a)
        {
            Exception expected = new Exception(a.Get);

            DeFuncto.Extensions.Tests.Try(() => { throw expected; return a; })
                .ShouldBeError(expected);
        }
    }
}
