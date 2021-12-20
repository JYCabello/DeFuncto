using FsCheck.Xunit;
using DeFuncto.Assertions;
using System;
using FsCheck;
using System.Threading.Tasks;
using Xunit;

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

            DeFuncto.Extensions.Tests.Try(() => ThisWillThrow(expected))
                .ShouldBeError(expected);
        }

        [Fact]
        public async Task TryAsyncOk()
        {
            var expected = Guid.NewGuid().ToString();
            await DeFuncto.Extensions.Tests.Try(() => Task.FromResult(expected))
                .ShouldBeOk(expected);
        }

        [Fact]
        public async Task TryAsyncError()
        {
            Exception expected = new Exception(Guid.NewGuid().ToString());

            await DeFuncto.Extensions.Tests.Try(async () => ThisWillThrow(expected))
                 .ShouldBeError(expected);
        }

        private int ThisWillThrow(Exception ex)
        {
            throw ex;
        }
    }
}
