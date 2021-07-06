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
        [InlineData(50, 50)]
        [InlineData(100, 10)]
        [InlineData(100, 150)]
        [InlineData(300, 150)]
        [InlineData(850, 150)]
        [InlineData(5_000, 100)]
        [InlineData(5_000, 300)]
        public async Task InParallel(int total, int parallelism)
        {
            var witness = new ConcurrentWitness();

            var results = await Enumerable.Range(0, total)
                .Select<int, Func<Task<int>>>(_ => async () =>
                {
                    using (witness.Grab())
                    {
                        var timesCalled = witness.TimesCalled;
                        await Task.Delay(TimeSpan.FromMilliseconds(1 + timesCalled % 25));
                        return timesCalled;
                    }
                }).Parallel(parallelism);

            var sum = results.Sum();
            Assert.True(total <= parallelism ? sum == 0 : total < sum);

            witness
                .ShouldBeenHeldMax(parallelism)
                .ShouldBeenHeldTotal(total);
        }
    }
}
