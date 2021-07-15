using DeFuncto.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks
{
    public class Select
    {
        [Fact(DisplayName = "Selects")]
        public async Task Selects()
        {
            var multipleLists = new[]
            {
                new Person { Name = "Jan" }.ToTask(),
                new Person { Name = "Henk" }.ToTask(),
                new Person { Name = "Klaas" }.ToTask(),
            }.Apply(Task.WhenAll);

            var result = await multipleLists.Select(p => p.Name);

            Assert.Equal(3, result.Count());
            Assert.Contains("Jan", result);
        }
    }

    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
