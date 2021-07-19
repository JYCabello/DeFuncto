using System.Threading.Tasks;
using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
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

        [Property(DisplayName = "Skips ok after error")]
        public void ErrorOk(string a) =>
            Error<int, string>(a)
                .Async()
                .Bind(_ => 42.Apply(Ok<int, string>))
                .ShouldBeError(a);

        [Property(DisplayName = "Skips error after error")]
        public void ErrorError(string a, string b) =>
            Error<int, string>(a)
                .Async()
                .Bind(_ => b.Apply(Error<int, string>))
                .ShouldBeError(a);

        [Property(DisplayName = "Asynchronously binds ok with ok")]
        public void AsyncOkOk(NonNull<string> a, NonNull<string> b) => 
            _ = ((AsyncResult<NonNull<string>, int>)a.ToTask())
               .Bind(val => $"{val}{b}".Apply(Ok<string, int>).Async())
               .ShouldBeOk($"{a}{b}")
               .Result;

        [Property(DisplayName = "Asynchronously binds ok with ok but as a task")]
        public void AsyncOkOkTask(string a, string b) => 
            _ = ((AsyncResult<string, int>)a.ToTask())
               .Bind(val => $"{val}{b}".Apply(Ok<string, int>).ToTask())
               .ShouldBeOk(a + b)
               .Result;

        [Property(DisplayName = "Asynchronously binds ok with error")]
        public void AsyncOkError(NonNull<string> a) => 
            _ = ((AsyncResult<int, string>)42)
                .Bind(_ => a.ToString().Apply(Error<int, string>).Async())
                .ShouldBeError(a.ToString())
                .Result;

        [Property(DisplayName = "Asynchronously skips ok after error")]
        public void AsyncErrorOk(NonNull<string> a) =>
            _ = ((AsyncResult<int, string>)a.ToString())
                .Bind(_ => 42.Apply(Ok<int, string>).Async())
                .ShouldBeError(a.ToString())
                .Result;

        [Property(DisplayName = "Asynchronously skips error after error")]
        public void AsyncErrorError(NonNull<string> a, string b) => 
            _ = ((AsyncResult<int, string>)a.ToString().ToTask())
                .Bind(_ => b.Apply(Error<int, string>).Async())
                .ShouldBeError(a.ToString())
                .Result;
    }
}
