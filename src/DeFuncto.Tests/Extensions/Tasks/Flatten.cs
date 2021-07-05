using System.Threading.Tasks;
using DeFuncto.Extensions;
using Xunit;

namespace DeFuncto.Tests.Extensions.Tasks
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
    }
}
