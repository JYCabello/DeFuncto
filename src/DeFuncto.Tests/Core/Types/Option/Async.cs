using DeFuncto.Assertions;
using DeFuncto.Extensions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.Option;

public class Async
{
    [Property(DisplayName = "From option of a task")]
    public void OptionOfTask(NonNull<string> a) =>
        _ = Some(a.Get.ToTask())
            .Async()
            .ShouldBeSome(a.Get)
            .Result;
}
