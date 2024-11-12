using System;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto.Assertions;

public static class OptionAssertions
{
  public static T ShouldBeSome<T>(this Option<T> option) =>
    option.Match(Id, () => throw new AssertionFailedException("Option should have been in the 'Some' state."));

  public static async Task<T> ShouldBeSome<T>(this AsyncOption<T> option) => await option.Option.Map(ShouldBeSome);

  public static T ShouldBeSome<T>(this Option<T> option, Func<T, Unit> assertion)
  {
    option.Iter(assertion);
    return option.ShouldBeSome();
  }

  public static async Task<T> ShouldBeSome<T>(this AsyncOption<T> option, Func<T, Unit> assertion)
  {
    await option.Iter(assertion);
    return await option.ShouldBeSome();
  }

  public static T ShouldBeSome<T>(this Option<T> option, T expected) =>
    option.ShouldBeSome().AssertEquals(expected);

  public static Task<T> ShouldBeSome<T>(this AsyncOption<T> option, T expected) =>
    option.ShouldBeSome().Map(t => t.AssertEquals(expected));

  public static Unit ShouldBeNone<T>(this Option<T> option) =>
    option.Match(_ => throw new AssertionFailedException("Option should have been in the 'None' state."), () => unit);

  public static async Task<Unit> ShouldBeNone<T>(this AsyncOption<T> option) => await option.Option.Map(ShouldBeNone);
}
