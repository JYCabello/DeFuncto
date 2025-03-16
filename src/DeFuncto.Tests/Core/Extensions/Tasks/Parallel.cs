using System;
using System.Linq;
using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks;

public class Parallel
{
    [Theory(DisplayName = "Executes actions in parallel")]
    [InlineData(10, 2)]
    [InlineData(10, 25)]
    [InlineData(50, 50)]
    [InlineData(100, 10)]
    [InlineData(100, 150)]
    [InlineData(150, 75)]
    [InlineData(500, 150)]
    public async Task InParallel(int total, int parallelism)
    {
        var witness = new ConcurrentWitness();

        Func<Task<int>> DelayWitness(int index) =>
            async () =>
            {
                using (witness.Grab())
                {
                    var timesCalled = witness.TimesCalled;
                    await Task.Delay(TimeSpan.FromMilliseconds(1 + index % 10 + timesCalled % 25));
                    return timesCalled;
                }
            };

        var results =
            await Enumerable
                .Range(0, total)
                .Select(DelayWitness)
                .Parallel(parallelism);

        var sum = results.Sum();
        Assert.True(total <= parallelism ? sum == 0 : total < sum);

        witness
            .ShouldBeenHeldMax(parallelism)
            .ShouldBeenHeldTotal(total);
    }
}
