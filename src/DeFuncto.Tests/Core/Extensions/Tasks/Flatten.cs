using System.Collections.Generic;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks
{
    public class Flatten
    {
        [Property(DisplayName = "Flattens")]
        public void Flattens(NonNull<string> a)
        {
            var nestedTask = a.Get.ToTask().ToTask();
            var result = nestedTask.Flatten().Result;
            Assert.Equal(a.Get, result);
        }

        [Property(DisplayName = "Flattens Array")]
        public void FlattensArray(int a, int b, int c, int d)
        {
            var multipleLists = new[]
            {
                new List<int> { a, b }.ToTask(),
                new List<int> { c, d }.ToTask(),
            }.Apply(Task.WhenAll);

            var singleList = multipleLists.Flatten().Result;

            Assert.Equal(4, singleList.Count);
            Assert.Contains(a, singleList);
            Assert.Contains(b, singleList);
            Assert.Contains(c, singleList);
            Assert.Contains(d, singleList);
        }
    }
}
