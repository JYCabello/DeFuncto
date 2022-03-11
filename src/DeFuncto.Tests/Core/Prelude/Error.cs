using DeFuncto.Assertions;
using FsCheck;
using FsCheck.Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Prelude;

public class Error
{
    [Property(DisplayName = "Instantiates a ResultError")]
    public void Works(NonNull<string> a) =>
        Error(a).Result<int>()
            .ShouldBeError(a);

    [Property(DisplayName = "Instantiates a Result that is an Error")]
    public void WorksWithBoth(NonNull<string> a) =>
        Error<int, NonNull<string>>(a)
            .ShouldBeError(a);
}
