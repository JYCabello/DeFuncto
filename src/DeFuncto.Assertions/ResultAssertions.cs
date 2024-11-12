using System;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto.Assertions;

public static class ResultAssertions
{
  public static TOk ShouldBeOk<TOk, TError>(this Result<TOk, TError> self) =>
    self
      .Match(
        ok => ok,
        _ => throw new AssertionFailedException("Result should be Ok, but it was Error")
      );

  public static Task<TOk> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self) => self.ToTask().Map(ShouldBeOk);

  public static TOk ShouldBeOk<TOk, TError>(this Result<TOk, TError> self, Func<TOk, Unit> assertion)
  {
    self.Iter(assertion);
    return self.ShouldBeOk();
  }

  public static async Task<TOk> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self, Func<TOk, Unit> assertion)
  {
    await self.Iter(assertion);
    return await self.ShouldBeOk();
  }

  public static TOk ShouldBeOk<TOk, TError>(this Result<TOk, TError> self, TOk expected) =>
    self.ShouldBeOk(val => val.AssertEquals(expected));

  public static Task<TOk> ShouldBeOk<TOk, TError>(this AsyncResult<TOk, TError> self, TOk expected) =>
    self.ToTask().Map(result => result.ShouldBeOk(ok => ok.AssertEquals(expected)));

  public static Unit AssertEquals<T>(this T self, T other)
  {
    if (!self.Equals(other))
      throw new AssertionFailedException($"Expected {other} but it was {self}");
    return unit;
  }

  public static TError ShouldBeError<TOk, TError>(this Result<TOk, TError> self) =>
    self
      .Match(
        _ => throw new AssertionFailedException("Result should be Error, but it was Ok"),
        error => error
      );

  public static Task<TError> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self) =>
    self.ToTask().Map(ShouldBeError);

  public static TError ShouldBeError<TOk, TError>(this Result<TOk, TError> self, Func<TError, Unit> assertion)
  {
    self.Iter(assertion);
    return self.ShouldBeError();
  }

  public static Task<TError> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self,
    Func<TError, Unit> assertion) => self.ToTask().Map(result => result.ShouldBeError(assertion));

  public static TError ShouldBeError<TOk, TError>(this Result<TOk, TError> self, TError expected) =>
    self.ShouldBeError(val => val.AssertEquals(expected));

  public static Task<TError> ShouldBeError<TOk, TError>(this AsyncResult<TOk, TError> self, TError expected) =>
    self.ToTask().Map(result => result.ShouldBeError(expected));
}
