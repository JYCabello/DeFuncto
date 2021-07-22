using FsCheck.Xunit;
using System.Collections.Generic;
using DeFuncto.Extensions;
using DeFuncto.Assertions;
using Xunit;
using System.Linq;

namespace DeFuncto.Tests.Core.Extensions.Collection
{
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

        [Property(DisplayName = "Get Some from IQueryable")]
        public void GetSomeFromIQueryable(int a) =>
            _ = new List<int> { a }
                .AsQueryable()
                .FirstOrNone()
                .ShouldBeSome(a);

        [Fact(DisplayName = "Get None from IQueryable")]
        public void GetNoneFromIQueryable() =>
            _ = new List<int>()
                .AsQueryable()
                .FirstOrNone()
                .ShouldBeNone();
    }
}
