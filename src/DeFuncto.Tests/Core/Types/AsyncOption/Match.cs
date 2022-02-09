using FsCheck.Xunit;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.Types.AsyncOption;

public class Match
{
    [Property(DisplayName = "Matches on Some")]
    public void OnSome(string a, string b)
    {
        AsyncOption<string> option = a;
        var result = option.Match(Id, () => b).Result;
        Assert.Equal(a, result);
    }

    [Property(DisplayName = "Matches on None")]
    public void OnNone(string a)
    {
        AsyncOption<string> option = None.Option<string>();
        var result = option.Match(Id, () => a).Result;
        Assert.Equal(a, result);
    }
}
