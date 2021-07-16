using DeFuncto.Extensions;
using FsCheck.Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks
{
    public class ToList
    {
        [Property]
        public void FromTask(int a, int b, int c)
        {
            var items = GetItems().ToTask().ToList().Result;

            Assert.IsType<List<int>>(items);
            Assert.Equal(3, items.Count);

            IEnumerable<int> GetItems() =>
                new List<int> { a, b, c };
        }
    }
}
