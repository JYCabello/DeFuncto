using System.Threading.Tasks;
using DeFuncto.Assertions;
using Xunit;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class Iter
    {
        [Fact(DisplayName = "Iterates on the OK side")]
        public async Task OnOk()
        {
            AsyncResult<string, int> ok = "banana";
            var witness = new Witness();

            await ok.Iter((string _) => witness.Touch());
            await ok.Iter((string _) => { witness.Touch(); });
            await ok.Iter((string _) =>
            {
                witness.Touch();
                return Task.CompletedTask;
            });

            witness.ShouldHaveBeenCalled(3);
            await ok.ShouldBeOk("banana");
        }
    }
}
