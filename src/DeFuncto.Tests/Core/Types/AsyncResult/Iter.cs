using System.Threading.Tasks;
using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace DeFuncto.Tests.Core.Types.AsyncResult
{
    public class Iter
    {
        [Property(DisplayName = "Iterates on the OK side and skips on error")]
        public void OnOk(NonNull<string> a)
        {
            AsyncResult<string, int> ok = a.Get;
            var witness = new Witness();

            _ = ok.Iter((string _) => witness.Touch()).Result;
            _ = ok.Iter((int _) => witness.Touch()).Result;
            _ = ok.Iter((string _) => { witness.Touch(); }).Result;
            _ = ok.Iter((string _) =>
            {
                witness.Touch();
                return Task.CompletedTask;
            }).Result;
            _ = ok.Iter((int _) =>
            {
                witness.Touch();
                return Task.CompletedTask;
            }).Result;

            witness.ShouldHaveBeenTouched(3);
            _ = ok.ShouldBeOk(a.Get).Result;
        }

        [Property(DisplayName = "Iterates on the Error side and skips on OK")]
        public void OnError(NonNull<string> a)
        {
            AsyncResult<int, string> ok = a.Get;
            var witness = new Witness();

            _ = ok.Iter((string _) => witness.Touch()).Result;
            _ = ok.Iter((int _) => witness.Touch()).Result;
            _ = ok.Iter((string _) => { witness.Touch(); }).Result;
            _ = ok.Iter((string _) =>
            {
                witness.Touch();
                return Task.CompletedTask;
            }).Result;
            _ = ok.Iter((int _) =>
            {
                witness.Touch();
                return Task.CompletedTask;
            }).Result;

            witness.ShouldHaveBeenTouched(3);
            _ = ok.ShouldBeError(a.Get).Result;
        }
    }
}
