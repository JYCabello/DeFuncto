using System;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto.Assertions;

/// <summary>
/// Assertion helpers for the Result and AsyncResult types.
/// </summary>
public static class ResultAssertions
{
  /// <summary>
  /// Asserts that the Result is in the Ok state and returns its value.
  /// </summary>
  /// <param name="self">The Result to assert on.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>The Ok value.</returns>
  public static TOk ShouldBeOk<TOk, TError>(this Result<TOk, TError> self) =>
    self
      .Match(
        ok => ok,
        _ => throw new AssertionFailedException("Result should be Ok, but it was Error")
      );

  /// <summary>
  /// Asserts that the AsyncResult is in the Ok state and returns its value.
  /// </summary>
  /// <param name="self">The AsyncResult to assert on.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>A task returning the Ok value.</returns>
  public static Task<TOk> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self) => self.ToTask().Map(ShouldBeOk);

  /// <summary>
  /// Asserts that the Result is in the Ok state, runs the assertion on its value and returns it.
  /// </summary>
  /// <param name="self">The Result to assert on.</param>
  /// <param name="assertion">Assertion to run on the Ok value.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>The Ok value.</returns>
  public static TOk ShouldBeOk<TOk, TError>(this Result<TOk, TError> self, Func<TOk, Unit> assertion)
  {
    self.Iter(assertion);
    return self.ShouldBeOk();
  }

  /// <summary>
  /// Asserts that the AsyncResult is in the Ok state, runs the assertion on its value and returns it.
  /// </summary>
  /// <param name="self">The AsyncResult to assert on.</param>
  /// <param name="assertion">Assertion to run on the Ok value.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>A task returning the Ok value.</returns>
  public static async Task<TOk> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self, Func<TOk, Unit> assertion)
  {
    await self.Iter(assertion);
    return await self.ShouldBeOk();
  }

  /// <summary>
  /// Asserts that the Result is in the Ok state with a value equal to the expected one and returns it.
  /// </summary>
  /// <param name="self">The Result to assert on.</param>
  /// <param name="expected">Expected Ok value.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>The Ok value.</returns>
  public static TOk ShouldBeOk<TOk, TError>(this Result<TOk, TError> self, TOk expected) =>
    self.ShouldBeOk(val => val.AssertEquals(expected).Apply(_ => unit));

  /// <summary>
  /// Asserts that the AsyncResult is in the Ok state with a value equal to the expected one and returns it.
  /// </summary>
  /// <param name="self">The AsyncResult to assert on.</param>
  /// <param name="expected">Expected Ok value.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>A task returning the Ok value.</returns>
  public static Task<TOk> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self, TOk expected) =>
    self.ToTask().Map(result => result.ShouldBeOk(ok => ok.AssertEquals(expected).Apply(_ => unit)));

  /// <summary>
  /// Asserts that the value is equal to another and returns it.
  /// </summary>
  /// <param name="self">The value to assert on.</param>
  /// <param name="other">The value to compare with.</param>
  /// <typeparam name="T">Type of the values.</typeparam>
  /// <returns>The asserted value.</returns>
  public static T AssertEquals<T>(this T self, T other)
  {
    if (!self.Equals(other))
      throw new AssertionFailedException($"Expected {other} but it was {self}");
    return self;
  }

  /// <summary>
  /// Asserts that the Result is in the Error state and returns its value.
  /// </summary>
  /// <param name="self">The Result to assert on.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>The Error value.</returns>
  public static TError ShouldBeError<TOk, TError>(this Result<TOk, TError> self) =>
    self
      .Match(
        _ => throw new AssertionFailedException("Result should be Error, but it was Ok"),
        error => error
      );

  /// <summary>
  /// Asserts that the AsyncResult is in the Error state and returns its value.
  /// </summary>
  /// <param name="self">The AsyncResult to assert on.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>A task returning the Error value.</returns>
  public static Task<TError> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self) =>
    self.ToTask().Map(ShouldBeError);

  /// <summary>
  /// Asserts that the Result is in the Error state, runs the assertion on its value and returns it.
  /// </summary>
  /// <param name="self">The Result to assert on.</param>
  /// <param name="assertion">Assertion to run on the Error value.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>The Error value.</returns>
  public static TError ShouldBeError<TOk, TError>(this Result<TOk, TError> self, Func<TError, Unit> assertion)
  {
    self.Iter(assertion);
    return self.ShouldBeError();
  }

  /// <summary>
  /// Asserts that the AsyncResult is in the Error state, runs the assertion on its value and returns it.
  /// </summary>
  /// <param name="self">The AsyncResult to assert on.</param>
  /// <param name="assertion">Assertion to run on the Error value.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>A task returning the Error value.</returns>
  public static Task<TError> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self,
    Func<TError, Unit> assertion) => self.ToTask().Map(result => result.ShouldBeError(assertion));

  /// <summary>
  /// Asserts that the Result is in the Error state with a value equal to the expected one and returns it.
  /// </summary>
  /// <param name="self">The Result to assert on.</param>
  /// <param name="expected">Expected Error value.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>The Error value.</returns>
  public static TError ShouldBeError<TOk, TError>(this Result<TOk, TError> self, TError expected) =>
    self.ShouldBeError(val => val.AssertEquals(expected).Apply(_ => unit));

  /// <summary>
  /// Asserts that the AsyncResult is in the Error state with a value equal to the expected one and returns it.
  /// </summary>
  /// <param name="self">The AsyncResult to assert on.</param>
  /// <param name="expected">Expected Error value.</param>
  /// <typeparam name="TOk">Type of the Ok value.</typeparam>
  /// <typeparam name="TError">Type of the Error value.</typeparam>
  /// <returns>A task returning the Error value.</returns>
  public static Task<TError> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self, TError expected) =>
    self.ToTask().Map(result => result.ShouldBeError(expected));
}
