using System.Threading.Tasks;

namespace DeFuncto.Tests.Core.Types.AsyncOption;

public class AsyncIterator
{
  [Fact(DisplayName = "Iterates over some")]
  public async Task Iterates()
  {
    var some = Some(1).Async();
    var count = 0;
    await foreach (var value in some)
    {
      Assert.Equal(1, value);
      count++;
    }
    Assert.Equal(1, count);
  }

  [Fact(DisplayName = "Does not iterate over none")]
  public async Task DoesNotIterate()
  {
    var none = Option<int>.None.Async();
    var count = 0;
    await foreach (var _ in none)
    {
      count++;
    }
    Assert.Equal(0, count);
  }
}
