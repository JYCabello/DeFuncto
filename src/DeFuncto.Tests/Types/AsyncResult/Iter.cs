using System.Threading.Tasks;
using DeFuncto.Assertions;
using Xunit;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class Iter
    {
        [Fact(DisplayName = "Iterates on the OK side and skips on error")]
        public async Task OnOk()
        {
            AsyncResult<string, int> ok = "banana";
            var witness = new Witness();

            await ok.Iter((string _) => witness.Touch());
            await ok.Iter((int _) => witness.Touch());
            await ok.Iter((string _) => { witness.Touch(); });
            await ok.Iter((string _) =>
            {
                witness.Touch();
                return Task.CompletedTask;
            });
            await ok.Iter((int _) =>
            {
                witness.Touch();
                return Task.CompletedTask;
            });

            witness.ShouldHaveBeenCalled(3);
            await ok.ShouldBeOk("banana");
        }

        [Fact(DisplayName = "Iterates on the Error side and skips on OK")]
        public async Task OnError()
        {
            AsyncResult<int, string> ok = "banana";
            var witness = new Witness();

            await ok.Iter((string _) => witness.Touch());
            await ok.Iter((int _) => witness.Touch());
            await ok.Iter((string _) => { witness.Touch(); });
            await ok.Iter((string _) =>
            {
                witness.Touch();
                return Task.CompletedTask;
            });
            await ok.Iter((int _) =>
            {
                witness.Touch();
                return Task.CompletedTask;
            });

            witness.ShouldHaveBeenCalled(3);
            await ok.ShouldBeError("banana");
        }
    }
}
