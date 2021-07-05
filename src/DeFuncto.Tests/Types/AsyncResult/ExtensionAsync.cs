using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Types.AsyncResult
{
    public class ExtensionAsync
    {
        [Fact(DisplayName = "Converts from task result on Ok")]
        public Task OnOk() =>
            Ok<string, int>("banana")
                .Apply(Task.FromResult)
                .Async()
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Converts from task as Ok on Ok")]
        public Task OnTaskOk() =>
            Ok<Task<string>, int>("banana".Apply(Task.FromResult))
                .Async()
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Converts from task as Ok on Error")]
        public Task OnTaskOkWithError() =>
            Error<Task<int>, string>("banana")
                .Async()
                .ShouldBeError("banana");

        [Fact(DisplayName = "Converts from task result on Error")]
        public Task OnError() =>
            Error<int, string>("banana")
                .Apply(Task.FromResult)
                .Async()
                .ShouldBeError("banana");

        [Fact(DisplayName = "Converts from task as Error on Error")]
        public Task OnTaskError() =>
            Error<int, Task<string>>("banana".Apply(Task.FromResult))
                .Async()
                .ShouldBeError("banana");

        [Fact(DisplayName = "Converts from task as Error on OK")]
        public Task OnTaskErrorWithOk() =>
            Ok<string, Task<int>>("banana")
                .Async()
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Converts from both Tasks on OK")]
        public Task OnBothTaskWithOk() =>
            Ok<Task<string>, Task<int>>("banana".Apply(Task.FromResult))
                .Async()
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Converts from both Tasks on Error")]
        public Task OnBothTaskWithError() =>
            Error<Task<int>, Task<string>>("banana".Apply(Task.FromResult))
                .Async()
                .ShouldBeError("banana");


        [Fact(DisplayName = "Converts from both Tasks and within a task on OK")]
        public Task OnAllTaskWithOk() =>
            Ok<Task<string>, Task<int>>("banana".Apply(Task.FromResult))
                .Apply(Task.FromResult)
                .Async()
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Converts from both Tasks and within a task on Error")]
        public Task OnAllTaskWithError() =>
            Error<Task<int>, Task<string>>("banana".Apply(Task.FromResult))
                .Apply(Task.FromResult)
                .Async()
                .ShouldBeError("banana");
    }
}
