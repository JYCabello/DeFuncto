using DeFuncto.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks
{
    public class Select
    {
        [Fact(DisplayName = "Selects from array")]
        public async Task FromArray()
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

        [Fact(DisplayName = "Selects from IEnumerable")]
        public async Task FromIEnumerable()
        {
            var result = await GetPeople().ToTask().Select(p => p.Name);

            Assert.Equal(3, result.Count());
            Assert.Contains("Jan", result);

            IEnumerable<Person> GetPeople() =>
                new List<Person>
                {
                    new Person { Name = "Jan" },
                    new Person { Name = "Henk" },
                    new Person { Name = "Klaas" }
                };
        }
    }

    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
