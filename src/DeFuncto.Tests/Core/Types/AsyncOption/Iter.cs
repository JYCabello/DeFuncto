using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class Iter
    {
        [Property(DisplayName = "Iterates on Some synchronously with both iterators")]
        public async void SomeSync(string a)
        {
            var some = Some(a).Async();
            var witness = new Witness();
            await some.Iter(
                _ => witness.Touch(),
                () => witness.Touch()
            );
            await some.Iter(() => witness.Touch());
            await some.Iter(_ => witness.Touch());
            witness.ShouldHaveBeenTouched(2);
        }

        [Property(DisplayName = "Iterates on None synchronously with both iterators")]
        public async void NoneSync(string a)
        {
            var some = Some(a).Async();
            var witness = new Witness();
            await some.Iter(
                _ => witness.Touch(),
                () => witness.Touch()
            );
            witness.ShouldHaveBeenTouched(1);
        }

        [Property(DisplayName = "Iterates on Some asynchronously with both iterators")]
        public async void SomeAsyncDouble(string a)
        {
            var some = Some(a).Async();
            var witness = new Witness();
            await some.Iter(
                _ => witness.Touch().ToTask(),
                () => witness.Touch().ToTask()
            );
            witness.ShouldHaveBeenTouched(1);
        }

        [Fact(DisplayName = "Iterates on None asynchronously")]
        public async Task NoneAsync()
        {
            var some = None.Option<string>().Async();
            var witness = new Witness();
            await some.Iter(() => witness.Touch().ToTask());
            await some.Iter(_ => witness.Touch().ToTask());
            witness.ShouldHaveBeenTouched(1);
        }
    }
}
