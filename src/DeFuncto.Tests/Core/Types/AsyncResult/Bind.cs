using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncResult
{
    public class Bind
    {
        [Property(DisplayName = "Binds ok with ok")]
        public void OkOk(string a, string b) =>
            Ok<string, int>(a)
                .Async()
                .Bind(val => (val + b).Apply(Ok<string, int>))
                .ShouldBeOk(a + b);

        [Property(DisplayName = "Binds ok with error")]
        public void OkError(string a) =>
            Ok<int, string>(42)
                .Async()
                .Bind(_ => a.Apply(Error<int, string>))
                .ShouldBeError(a);

        [Fact(DisplayName = "Skips ok after error")]
        public Task ErrorOk() =>
            Error<int, string>("banana")
                .Async()
                .Bind(_ => 42.Apply(Ok<int, string>))
                .ShouldBeError("banana");

        [Fact(DisplayName = "Skips error after error")]
        public Task ErrorError() =>
            Error<int, string>("banana")
                .Async()
                .Bind(_ => "pear".Apply(Error<int, string>))
                .ShouldBeError("banana");

        [Fact(DisplayName = "Asynchronously binds ok with ok")]
        public Task AsyncOkOk() =>
            ((AsyncResult<string, int>) "ban".ToTask())
            .Bind(val => $"{val}ana".Apply(Ok<string, int>).Async())
            .ShouldBeOk("banana");

        [Fact(DisplayName = "Asynchronously binds ok with ok but as a task")]
        public Task AsyncOkOkTask() =>
            ((AsyncResult<string, int>) "ban".ToTask())
            .Bind(val => $"{val}ana".Apply(Ok<string, int>).ToTask())
            .ShouldBeOk("banana");

        [Fact(DisplayName = "Asynchronously binds ok with error")]
        public Task AsyncOkError() =>
            ((AsyncResult<int, string>) 42)
                .Bind(_ => "banana".Apply(Error<int, string>).Async())
                .ShouldBeError("banana");

        [Fact(DisplayName = "Asynchronously skips ok after error")]
        public Task AsyncErrorOk() =>
            ((AsyncResult<int, string>) "banana")
                .Bind(_ => 42.Apply(Ok<int, string>).Async())
                .ShouldBeError("banana");

        [Fact(DisplayName = "Asynchronously skips error after error")]
        public Task AsyncErrorError() =>
            ((AsyncResult<int, string>) "banana".ToTask())
                .Bind(_ => "pear".Apply(Error<int, string>).Async())
                .ShouldBeError("banana");
    }
}
