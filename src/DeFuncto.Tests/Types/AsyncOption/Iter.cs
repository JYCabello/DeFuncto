using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncOption
{
    public class Iter
    {
        [Fact(DisplayName = "Iterates on Some synchronously with both iterators")]
        public async Task SomeSync()
        {
            var some = Some("banana").Async();
            var witness = new Witness();
            await some.Iter(
                _ => witness.Touch(),
                () => witness.Touch()
            );
            await some.Iter(() => witness.Touch());
            await some.Iter(_ => witness.Touch());
            witness.ShouldHaveBeenTouched(2);
        }

        [Fact(DisplayName = "Iterates on None synchronously with both iterators")]
        public async Task NoneSync()
        {
            var some = Some("banana").Async();
            var witness = new Witness();
            await some.Iter(
                _ => witness.Touch(),
                () => witness.Touch()
            );
            witness.ShouldHaveBeenTouched(1);
        }

        [Fact(DisplayName = "Iterates on Some asynchronously with both iterators")]
        public async Task SomeAsyncDouble()
        {
            var some = Some("banana").Async();
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
