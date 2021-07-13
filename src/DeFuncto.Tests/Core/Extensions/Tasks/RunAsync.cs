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
            await witness.RunAsync(async w => w.Touch());
            await witness.RunAsync(async w => { w.Touch(); });
            await witness.ToTask().RunAsync(async w => { w.Touch(); });
            Assert.Equal(3, witness.TimesCalled);
        }
    }
}
