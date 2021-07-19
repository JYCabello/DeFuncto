using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption
{
    public class Map
    {
        [Property(DisplayName = "Maps Some")]
        public void OnSome(string a, string b) =>
            Some(a)
                .Async()
                .Map(val => $"{val}{b}")
                .ShouldBeSome($"{a}{b}")
                .SyncVoid();

        [Property(DisplayName = "Maps Some async")]
        public void OnSomeAsync(string a, string b) =>
            Some(a)
                .Async()
                .Map(val => $"{val}{b}".ToTask())
                .ShouldBeSome($"{a}{b}")
                .SyncVoid();

        [Property(DisplayName = "Skips on None")]
        public void OnNone(string a) =>
            None.Option<Task<string>>().Async()
                .Map(_ => a)
                .ShouldBeNone()
                .SyncVoid();

        [Property(DisplayName = "Skips on None async")]
        public void OnNoneAsync(string a) =>
            None.Option<Task<string>>().Async()
                .Map(_ => a.ToTask())
                .ShouldBeNone()
                .SyncVoid();
    }
}
