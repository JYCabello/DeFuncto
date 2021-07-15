using System.Collections.Generic;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks
{
    public class Flatten
    {
        [Fact(DisplayName = "Flattens")]
        public async Task Flattens()
        {
            var nestedTask = "banana".ToTask().ToTask();
            var result = await nestedTask.Flatten();
            Assert.Equal("banana", result);
        }

        [Fact(DisplayName = "Flattens Array")]
        public async Task FlattensArray()
        {
            var multipleLists = new[]
            {
                new List<int> { 1, 2, 3, 4, 5 }.ToTask(),
                new List<int> { 1, 2, 3, 4, 5 }.ToTask(),
            }.Apply(Task.WhenAll);

            var singleList = await multipleLists.Flatten();

            Assert.Equal(10, singleList.Count);
        }
    }
}
