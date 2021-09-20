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
        public void Works(NonNull<string> val)
        {
            var witness = "unaware";
            Func<Task> touch = () =>
            {
                witness = "aware";
                return Task.CompletedTask;
            };
            Thread.Sleep(3);
            Assert.Equal("unaware", witness);
            var touchFunction = touch.AsyncFunction();
            Thread.Sleep(3);
            Assert.Equal("unaware", witness);
            touchFunction().Wait();
            Assert.Equal("aware", witness);
        }
    }
}
