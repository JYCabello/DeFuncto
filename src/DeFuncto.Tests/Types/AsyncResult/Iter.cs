using System.Threading.Tasks;
using DeFuncto.Assertions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class Iter
    {
        [Fact(DisplayName = "Iterates on the OK side")]
        public async Task OnOk()
        {
            AsyncResult<string, int> ok = "banana";
            var witness = new Witness();

            witness.ShouldHaveBeenCalled(1);
            await ok.ShouldBeOk("banana");
        }
    }
}
