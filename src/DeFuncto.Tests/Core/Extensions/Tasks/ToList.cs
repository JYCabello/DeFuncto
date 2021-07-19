using DeFuncto.Extensions;
using FsCheck.Xunit;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks
{
    public class ToList
    {
        [Property]
        public void IEnumerableFromTask(int a, int b, int c)
        {
            var items = GetItems().ToTask().ToList().Result;

            Assert.IsType<List<int>>(items);
            Assert.Equal(3, items.Count);

            IEnumerable<int> GetItems() =>
                new List<int> { a, b, c };
        }

        [Property]
        public void ArrayFromTask(int a, int b, int c)
        {
            var items = GetItems().ToTask().ToList().Result;

            Assert.IsType<List<int>>(items);
            Assert.Equal(3, items.Count);

            int[] GetItems() =>
                new int[]{ a, b, c };
        }
    }
}
