using System;
using System.Linq;
using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Extensions.Tasks
{
    public class Parallel
    {
        [Theory(DisplayName = "Executes actions in parallel")]
        [InlineData(10, 2)]
        [InlineData(10, 25)]
        [InlineData(100, 10)]
        [InlineData(850, 150)]
        [InlineData(5_000, 100)]
        public async Task InParallel(int total, int parallelism)
        {
            var witness = new ConcurrentWitness();
            await Enumerable.Range(0, total)
                .Select<int, Func<Task<Unit>>>(_ => async () =>
                {
                    using (witness.Grab())
                    {
                        await Task.Delay(TimeSpan.FromTicks(1));
                        return unit;
                    }
                }).Parallel(parallelism);
            witness
                .ShouldBeenHeldMax(parallelism)
                .ShouldBeenHeldTotal(total);
        }
    }
}
