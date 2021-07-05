using System.Linq;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Extensions.Collection
{
    public class Choose
    {
        [Theory(DisplayName = "Selects only on some")]
        [InlineData(265_166, 212)]
        [InlineData(500_000, 13)]
        [InlineData(125_124, 4)]
        public void Chooses(int amount, int moduloOf)
        {
            var result = Enumerable
                .Range(0, amount)
                .Choose(n => n % moduloOf == 0 ? Some(n) : None)
                .ToList();

            Assert.All(result, n => Assert.Equal(0, n % moduloOf));
            if (amount % moduloOf == 0)
                Assert.Equal(amount / moduloOf, result.Count);
            else
                Assert.Equal(amount / moduloOf + 1, result.Count);
        }
    }
}
