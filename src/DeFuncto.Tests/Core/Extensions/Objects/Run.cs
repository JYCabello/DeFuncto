using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Objects
{
    public class Run
    {
        [Fact(DisplayName = "Runs an action and a function")]
        public void Runs()
        {
            var witness = new Witness();
            witness
                .Run(w => w.Touch())
                .Run(w => { w.Touch(); });
            Assert.Equal(2, witness.TimesCalled);
        }
    }
}
