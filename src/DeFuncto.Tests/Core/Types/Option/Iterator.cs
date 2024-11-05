using System.Linq;
using Xunit;

namespace DeFuncto.Tests.Core.Types.Option;

public class Iterator
{
  [Fact(DisplayName = "Iterates over some")]
  public void Iterates()
  {
    var some = Some(1);
    var count = 0;
    foreach (var value in some)
    {
      Assert.Equal(1, value);
      count++;
    }
    Assert.Equal(1, count);
  }

  [Fact(DisplayName = "Does not iterate over none")]
  public void DoesNotIterate()
  {
    Option<int> none = None;
    var count = none.Count();
    Assert.Equal(0, count);
  }
}
