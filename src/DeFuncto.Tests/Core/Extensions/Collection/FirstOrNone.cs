using System.Collections.Generic;
using System.Linq;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;

namespace DeFuncto.Tests.Core.Extensions.Collection;

public class FirstOrNone
{
    [Property(DisplayName = "Get Some from List")]
    public void GetSomeFromList(int a) =>
        _ = new List<int> { a }
            .FirstOrNone()
            .ShouldBeSome(a);

    [Fact(DisplayName = "Get None from List")]
    public void GetNoneFromList() =>
        _ = new List<int>()
            .FirstOrNone()
            .ShouldBeNone();

    [Property(DisplayName = "Get Some from IEnumerable")]
    public void GetSomeFromIEnumerable(int a) =>
        _ = new List<int> { a }
            .AsEnumerable()
            .FirstOrNone()
            .ShouldBeSome(a);

    [Fact(DisplayName = "Get None from IEnumerable")]
    public void GetNoneFromIEnumerable() =>
        _ = new List<int>()
            .AsEnumerable()
            .FirstOrNone()
            .ShouldBeNone();

    [Property(DisplayName = "Get Some from List")]
    public void GetSomeFromListWithPredicate(int a) =>
        _ = new List<int> { a }
            .FirstOrNone(x => x == a)
            .ShouldBeSome(a);

    [Fact(DisplayName = "Get None from List")]
    public void GetNoneFromListWithPredicate() =>
        _ = new List<int>()
            .FirstOrNone(x => x == 1)
            .ShouldBeNone();

    [Property(DisplayName = "Get Some from IEnumerable")]
    public void GetSomeFromIEnumerableWithPredicate(int a) =>
        _ = new List<int> { a }
            .AsEnumerable()
            .FirstOrNone(x => x == a)
            .ShouldBeSome(a);

    [Fact(DisplayName = "Get None from IEnumerable")]
    public void GetNoneFromIEnumerableWithPredicate() =>
        _ = new List<int>()
            .AsEnumerable()
            .FirstOrNone(x => x == 1)
            .ShouldBeNone();
}
