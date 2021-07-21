using FsCheck.Xunit;
using System.Collections.Generic;
using DeFuncto.Extensions;
using DeFuncto.Assertions;
using Xunit;
using System.Linq;

namespace DeFuncto.Tests.Core.Extensions.Collection
{
    public class SingleOrNone
    {
        [Property(DisplayName = "Get Some from List")]
        public void GetSomeFromList(int a) =>
            _ = new List<int> { a }
                .SingleOrNone()
                .ShouldBeSome(a);

        [Fact(DisplayName = "Get None from List")]
        public void GetNoneFromList() =>
            _ = new List<int>()
                .SingleOrNone()
                .ShouldBeNone();

        [Property(DisplayName = "Get Some from IEnumerable")]
        public void GetSomeFromIEnumerable(int a) =>
            _ = new List<int> { a }
                .AsEnumerable()
                .SingleOrNone()
                .ShouldBeSome(a);

        [Fact(DisplayName = "Get None from IEnumerable")]
        public void GetNoneFromIEnumerable() =>
            _ = new List<int>()
                .AsEnumerable()
                .SingleOrNone()
                .ShouldBeNone();

        [Property(DisplayName = "Get Some from IQueryable")]
        public void GetSomeFromIQueryable(int a) =>
            _ = new List<int> { a }
                .AsQueryable()
                .SingleOrNone()
                .ShouldBeSome(a);

        [Fact(DisplayName = "Get None from IQueryable")]
        public void GetNoneFromIQueryable() =>
            _ = new List<int>()
                .AsQueryable()
                .SingleOrNone()
                .ShouldBeNone();
    }
}
