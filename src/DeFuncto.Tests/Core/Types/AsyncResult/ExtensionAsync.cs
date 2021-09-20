using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
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

        [Property(DisplayName = "Converts from task as Ok on Ok")]
        public void OnTaskOk(NonNull<string> a) =>
            _ = Ok<Task<string>, int>(a.Get.ToTask())
                .Async()
                .ShouldBeOk(a.Get)
                .Result;

        [Property(DisplayName = "Converts from task as Ok on Error")]
        public void OnTaskOkWithError(NonNull<string> a) =>
            _ = Error<Task<int>, string>(a.Get)
                .Async()
                .ShouldBeError(a.Get)
                .Result;

        [Property(DisplayName = "Converts from task result on Error")]
        public void OnError(NonNull<string> a) =>
            _ = Error<int, string>(a.Get)
                .ToTask()
                .Async()
                .ShouldBeError(a.Get)
                .Result;

        [Property(DisplayName = "Converts from task as Error on Error")]
        public void OnTaskError(NonNull<string> a) =>
            _ = Error<int, Task<string>>(a.Get.ToTask())
                .Async()
                .ShouldBeError(a.Get)
            .Result;

        [Property(DisplayName = "Converts from task as Error on OK")]
        public void OnTaskErrorWithOk(NonNull<string> a) =>
            _ = Ok<string, Task<int>>(a.Get)
                .Async()
                .ShouldBeOk(a.Get)
                .Result;

        [Property(DisplayName = "Converts from both Tasks on OK")]
        public void OnBothTaskWithOk(NonNull<string> a) =>
            _ = Ok<Task<string>, Task<int>>(a.Get.ToTask())
                .Async()
                .ShouldBeOk(a.Get)
                .Result;

        [Property(DisplayName = "Converts from both Tasks on Error")]
        public void OnBothTaskWithError(NonNull<string> a) =>
            _ = Error<Task<int>, Task<string>>(a.Get.ToTask())
                .Async()
                .ShouldBeError(a.Get)
                .Result;


        [Property(DisplayName = "Converts from both Tasks and within a task on OK")]
        public void OnAllTaskWithOk(NonNull<string> a) =>
            _ = Ok<Task<string>, Task<int>>(a.Get.ToTask())
                .ToTask()
                .Async()
                .ShouldBeOk(a.Get)
                .Result;

        [Property(DisplayName = "Converts from both Tasks and within a task on Error")]
        public void OnAllTaskWithError(NonNull<string> a) =>
            _ = Error<Task<int>, Task<string>>(a.Get.ToTask())
                .ToTask()
                .Async()
                .ShouldBeError(a.Get)
                .Result;
    }
}
