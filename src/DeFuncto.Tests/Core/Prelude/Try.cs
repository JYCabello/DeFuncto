using FsCheck.Xunit;
using DeFuncto.Assertions;
using System;
using FsCheck;
using System.Threading.Tasks;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Tries
{
    public class Try
    {
        [Property]
        public void TryOk(int a) =>
            Try(() => a)
                .ShouldBeOk(a);

        [Property]
        public void TryError(NonNull<string> a)
        {
            Exception expected = new Exception(a.Get);

            Try(() => ThisWillThrow(expected))
                .ShouldBeError(expected);
        }

        [Fact]
        public async Task TryAsyncOk()
        {
            var expected = Guid.NewGuid().ToString();
            await Try(() => Task.FromResult(expected))
                .ShouldBeOk(expected);
        }

        [Fact]
        public async Task TryAsyncError()
        {
            Exception expected = new Exception(Guid.NewGuid().ToString());

            await Try(async () => ThisWillThrow(expected))
                 .ShouldBeError(expected);
        }

        private int ThisWillThrow(Exception ex)
        {
            throw ex;
        }
    }
}
