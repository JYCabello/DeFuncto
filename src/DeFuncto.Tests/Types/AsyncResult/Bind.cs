using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class Bind
    {
        [Fact(DisplayName = "Binds ok with ok")]
        public Task OkOk() =>
            Ok<string, int>("ban")
                .Async()
                .Bind(val => $"{val}ana".Apply(Ok<string, int>))
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Binds ok with error")]
        public Task OkError() =>
            Ok<int, string>(42)
                .Async()
                .Bind(_ => "banana".Apply(Error<int, string>))
                .ShouldBeError("banana");

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
            ((AsyncResult<string, int>) "ban".Apply(Task.FromResult))
            .Bind(val => $"{val}ana".Apply(Ok<string, int>).Async())
            .ShouldBeOk("banana");

        [Fact(DisplayName = "Asynchronously binds ok with ok but as a task")]
        public Task AsyncOkOkTask() =>
            ((AsyncResult<string, int>) "ban".Apply(Task.FromResult))
            .Bind(val => $"{val}ana".Apply(Ok<string, int>).Apply(Task.FromResult))
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
            ((AsyncResult<int, string>) "banana".Apply(Task.FromResult))
                .Bind(_ => "pear".Apply(Error<int, string>).Async())
                .ShouldBeError("banana");
    }
}
