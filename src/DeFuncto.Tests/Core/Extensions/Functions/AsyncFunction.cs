using System;
using System.Threading;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Functions
{
    public class AsyncFunction
    {
        [Property(DisplayName = "Creates an async function that does the same")]
        public void Works(NonNull<string> a, NonNull<string> b)
        {
            var witness = a.Get;
            Func<Task> touch = () =>
            {
                witness = b.Get;
                return Task.CompletedTask;
            };
            Thread.Sleep(1);
            Assert.Equal(a.Get, witness);
            var touchFunction = touch.AsyncFunction();
            Thread.Sleep(1);
            Assert.Equal(a.Get, witness);
            touchFunction().Wait();
            Assert.Equal(b.Get, witness);
        }
    }
}
