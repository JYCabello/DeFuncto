using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncResult
{
    public class ExtensionAsync
    {
        [Property(DisplayName = "Converts from task result on Ok")]
        public void OnOk(NonNull<string> a) =>
            _ = Ok<string, int>(a.Get)
                .ToTask()
                .Async()
                .ShouldBeOk(a.Get)
                .Result;

        [Fact(DisplayName = "Converts from task as Ok on Ok")]
        public Task OnTaskOk() =>
            Ok<Task<string>, int>("banana".ToTask())
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
                .ToTask()
                .Async()
                .ShouldBeError("banana");

        [Fact(DisplayName = "Converts from task as Error on Error")]
        public Task OnTaskError() =>
            Error<int, Task<string>>("banana".ToTask())
                .Async()
                .ShouldBeError("banana");

        [Fact(DisplayName = "Converts from task as Error on OK")]
        public Task OnTaskErrorWithOk() =>
            Ok<string, Task<int>>("banana")
                .Async()
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Converts from both Tasks on OK")]
        public Task OnBothTaskWithOk() =>
            Ok<Task<string>, Task<int>>("banana".ToTask())
                .Async()
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Converts from both Tasks on Error")]
        public Task OnBothTaskWithError() =>
            Error<Task<int>, Task<string>>("banana".ToTask())
                .Async()
                .ShouldBeError("banana");


        [Fact(DisplayName = "Converts from both Tasks and within a task on OK")]
        public Task OnAllTaskWithOk() =>
            Ok<Task<string>, Task<int>>("banana".ToTask())
                .ToTask()
                .Async()
                .ShouldBeOk("banana");

        [Fact(DisplayName = "Converts from both Tasks and within a task on Error")]
        public Task OnAllTaskWithError() =>
            Error<Task<int>, Task<string>>("banana".ToTask())
                .ToTask()
                .Async()
                .ShouldBeError("banana");
    }
}
