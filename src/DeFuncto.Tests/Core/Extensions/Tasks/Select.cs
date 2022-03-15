using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Tasks;

public class Select
{
    [Property(DisplayName = "Selects from array")]
    public void FromArray(Person a, Person b, Person c)
    {
        var multipleLists = new[]
        {
            a.ToTask(),
            b.ToTask(),
            c.ToTask()
        }.Apply(Task.WhenAll);

        var result = multipleLists.Select(p => p.Name).Result;
        var asArray = result.ToArray();

        Assert.Equal(3, asArray.Length);
        Assert.Contains(a.Name, asArray);
    }

    [Property(DisplayName = "Selects from IEnumerable")]
    public void FromIEnumerable(Person a, Person b, Person c)
    {
        var result = GetPeople().ToTask().Select(p => p.Age).Result;
        var asArray = result.ToArray();

        Assert.Equal(3, asArray.Length);
        Assert.Contains(a.Age, asArray);

        IEnumerable<Person> GetPeople() =>
            new List<Person> { a, b, c };
    }

    [Property(DisplayName = "Selects from IEnumerable")]
    public void FromList(Person a, Person b, Person c)
    {
        var result = GetPeople().ToTask().Select(p => p.Age).Result;
        var asArray = result.ToArray();

        Assert.Equal(3, asArray.Length);
        Assert.Contains(a.Age, asArray);

        List<Person> GetPeople() => new() { a, b, c };
    }

    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
