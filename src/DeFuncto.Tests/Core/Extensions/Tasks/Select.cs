using DeFuncto.Extensions;
using FsCheck.Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks
{
    public class Select
    {
        [Property(DisplayName = "Selects from array")]
        public void FromArray(Person a, Person b, Person c)
        {
            var multipleLists = new[]
            {
                a.ToTask(),
                b.ToTask(),
                c.ToTask(),
            }.Apply(Task.WhenAll);

            var result = multipleLists.Select(p => p.Name).Result;

            Assert.Equal(3, result.Count());
            Assert.Contains(a.Name, result);
        }

        [Property(DisplayName = "Selects from IEnumerable")]
        public void FromIEnumerable(Person a, Person b, Person c)
        {
            var result = GetPeople().ToTask().Select(p => p.Name).Result;

            Assert.Equal(3, result.Count());
            Assert.Contains(a.Name, result);

            IEnumerable<Person> GetPeople() =>
                new List<Person> { a, b, c };
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
