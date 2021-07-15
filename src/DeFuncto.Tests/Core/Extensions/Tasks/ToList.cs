using DeFuncto.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks
{
    public class ToList
    {
        [Fact]
        public async Task FromTask()
        {
            var items = await GetItems().ToTask().ToList();

            Assert.IsType<List<int>>(items);

            IEnumerable<int> GetItems() =>
                new List<int> { 1, 2, 3, 4, 5 };
        }
    }
}
