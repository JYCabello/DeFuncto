using System.Threading.Tasks;
using DeFuncto.Extensions;
using Xunit;

namespace DeFuncto.Tests.Core.Booleans
{
    public class Match
    {
        [Fact]
        public void PositiveMatchFunction()
        {
            string result = true.Match(() => "A", () => "B");
            Assert.Equal("A", result);
        }

        [Fact]
        public void NegativeMatchFunction()
        {
            string result = false.Match(() => "A", () => "B");
            Assert.Equal("B", result);
        }

        [Fact]
        public async Task PositiveTaskMatchFunction()
        {
            string result = await true.ToTask().Match(() => "A", () => "B");
            Assert.Equal("A", result);
        }

        [Fact]
        public async Task NegativeTaskMatchFunction()
        {
            string result = await false.ToTask().Match(() => "A", () => "B");
            Assert.Equal("B", result);
        }

        [Fact]
        public async Task PositiveTaskMatchFunctionTask()
        {
            string result = await true.ToTask().Match(() => "A".ToTask(), () => "B".ToTask());
            Assert.Equal("A", result);
        }

        [Fact]
        public async Task NegativeTaskMatchFunctionTask()
        {
            string result = await false.ToTask().Match(() => "A".ToTask(), () => "B".ToTask());
            Assert.Equal("B", result);
        }
    }
}
