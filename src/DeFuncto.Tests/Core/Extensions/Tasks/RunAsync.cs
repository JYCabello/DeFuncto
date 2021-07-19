using DeFuncto.Assertions;
using DeFuncto.Extensions;
using System.Threading.Tasks;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks
{
    public class RunAsync
    {
        [Fact(DisplayName = "Runs an asynchronous action")]
        public async Task Runs()
        {
            var witness = new Witness();
            await witness.RunAsync(
#pragma warning disable 1998
                async w => w.Touch()
#pragma warning restore 1998
                );

            await witness.RunAsync(
#pragma warning disable 1998
                async w =>
            {
                w.Touch();
            }
#pragma warning restore 1998
            );
            await witness.ToTask().RunAsync(
#pragma warning disable 1998
                async w =>
                {
                    w.Touch();
                }
#pragma warning restore 1998
                );
            Assert.Equal(3, witness.TimesCalled);
        }
    }
}
