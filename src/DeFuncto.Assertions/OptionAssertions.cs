using System;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto.Assertions;

/// <summary>
/// Assertion extensions for the <see cref="Option{T}"/> and <see cref="AsyncOption{T}"/> types.
/// </summary>
public static class OptionAssertions
{
  /// <summary>
  /// Asserts that the Option is in the Some state and returns its value.
  /// </summary>
  /// <param name="option">Option to assert on.</param>
  /// <typeparam name="T">Type of the contained value.</typeparam>
  /// <returns>The contained value.</returns>
  public static T ShouldBeSome<T>(this Option<T> option) =>
    option.Match(Id, () => throw new AssertionFailedException("Option should have been in the 'Some' state."));

  /// <summary>
  /// Asserts that the AsyncOption is in the Some state and returns its value.
  /// </summary>
  /// <param name="option">AsyncOption to assert on.</param>
  /// <typeparam name="T">Type of the contained value.</typeparam>
  /// <returns>A task with the contained value.</returns>
  public static async Task<T> ShouldBeSome<T>(this AsyncOption<T> option) => await option.Option.Map(ShouldBeSome);

  /// <summary>
  /// Asserts that the Option is in the Some state, runs the assertion on its value and returns it.
  /// </summary>
  /// <param name="option">Option to assert on.</param>
  /// <param name="assertion">Assertion to run on the contained value.</param>
  /// <typeparam name="T">Type of the contained value.</typeparam>
  /// <returns>The contained value.</returns>
  public static T ShouldBeSome<T>(this Option<T> option, Func<T, Unit> assertion)
  {
    option.Iter(assertion);
    return option.ShouldBeSome();
  }

  /// <summary>
  /// Asserts that the AsyncOption is in the Some state, runs the assertion on its value and returns it.
  /// </summary>
  /// <param name="option">AsyncOption to assert on.</param>
  /// <param name="assertion">Assertion to run on the contained value.</param>
  /// <typeparam name="T">Type of the contained value.</typeparam>
  /// <returns>A task with the contained value.</returns>
  public static async Task<T> ShouldBeSome<T>(this AsyncOption<T> option, Func<T, Unit> assertion)
  {
    await option.Iter(assertion);
    return await option.ShouldBeSome();
  }

  /// <summary>
  /// Asserts that the Option is in the Some state and holds the expected value.
  /// </summary>
  /// <param name="option">Option to assert on.</param>
  /// <param name="expected">Expected contained value.</param>
  /// <typeparam name="T">Type of the contained value.</typeparam>
  /// <returns>The contained value.</returns>
  public static T ShouldBeSome<T>(this Option<T> option, T expected) =>
    option.ShouldBeSome().AssertEquals(expected);

  /// <summary>
  /// Asserts that the AsyncOption is in the Some state and holds the expected value.
  /// </summary>
  /// <param name="option">AsyncOption to assert on.</param>
  /// <param name="expected">Expected contained value.</param>
  /// <typeparam name="T">Type of the contained value.</typeparam>
  /// <returns>A task with the contained value.</returns>
  public static Task<T> ShouldBeSome<T>(this AsyncOption<T> option, T expected) =>
    option.ShouldBeSome().Map(t => t.AssertEquals(expected));

  /// <summary>
  /// Asserts that the Option is in the None state.
  /// </summary>
  /// <param name="option">Option to assert on.</param>
  /// <typeparam name="T">Type of the contained value.</typeparam>
  /// <returns>Unit.</returns>
  public static Unit ShouldBeNone<T>(this Option<T> option) =>
    option.Match(_ => throw new AssertionFailedException("Option should have been in the 'None' state."), () => unit);

  /// <summary>
  /// Asserts that the AsyncOption is in the None state.
  /// </summary>
  /// <param name="option">AsyncOption to assert on.</param>
  /// <typeparam name="T">Type of the contained value.</typeparam>
  /// <returns>A task with Unit.</returns>
  public static async Task<Unit> ShouldBeNone<T>(this AsyncOption<T> option) => await option.Option.Map(ShouldBeNone);
}
