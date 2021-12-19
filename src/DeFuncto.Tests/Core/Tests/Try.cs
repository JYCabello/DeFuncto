using FsCheck.Xunit;
using DeFuncto.Assertions;
using Xunit;
using System;
using FsCheck;
using System.Threading.Tasks;

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

        [Property]
        public void TryAsyncOk(int a) =>
            DeFuncto.Extensions.Tests.Try(() => Task.FromResult(a))
                .ShouldBeOk(a);

        [Property]
        public void TryAsyncError(NonNull<string> a)
        {
            Exception expected = new Exception(a.Get);

            DeFuncto.Extensions.Tests.Try(async () => { throw expected; return a; })
                .ShouldBeError(expected);
        }
    }
}
