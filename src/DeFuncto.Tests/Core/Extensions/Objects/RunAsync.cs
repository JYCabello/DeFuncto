using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Objects
{
    public class RunAsync
    {

        [Fact(DisplayName = "Runs an asynchronous action")]
        public void Runs()
        {
            var witness = new Witness();
#pragma warning disable 1998
            witness.RunAsync(async w => w.Touch());
            witness.RunAsync(async w => { w.Touch(); });
#pragma warning restore 1998
            Assert.Equal(2, witness.TimesCalled);
        }
    }
}
