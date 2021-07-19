using DeFuncto.Extensions;
using FsCheck.Xunit;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks
{
    public class ToArray
    {
        [Property]
        public void FromTaskIEnumerable(int a, int b, int c)
        {
            var items = GetItems().ToTask().ToArray().Result;

            Assert.IsType<int[]>(items);
            Assert.Equal(3, items.Length);

            IEnumerable<int> GetItems() =>
                new List<int> { a, b, c };
        }

        [Property]
        public void FromTaskList(int a, int b, int c)
        {
            var items = GetItems().ToTask().ToArray().Result;

            Assert.IsType<int[]>(items);
            Assert.Equal(3, items.Length);

            List<int> GetItems() =>
                new List<int> { a, b, c };
        }
    }
}
