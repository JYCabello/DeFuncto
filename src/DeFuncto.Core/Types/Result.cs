using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto;

/// <summary>
/// Discriminated union representing the result of a calculation,
/// with an Ok state for the success and an Error state for the failure.
/// Biased towards the Ok case.
/// </summary>
/// <typeparam name="TOk">Error type.</typeparam>
/// <typeparam name="TError">Value type.</typeparam>
public readonly struct Result<TOk, TError> : IEquatable<Result<TOk, TError>>
{
    private readonly Du<TOk, TError> value;

    /// <summary>
    /// True for the Ok state.
    /// </summary>
    public readonly bool IsOk;

    /// <summary>
    /// True for the Error state.
    /// </summary>
    public bool IsError => !IsOk;

    private Result(TError error)
    {
        value = Second<TOk, TError>(error);
        IsOk = false;
    }

    private Result(TOk ok)
    {
        value = First<TOk, TError>(ok);
        IsOk = true;
    }

    /// <summary>
    /// Static constructor for the Ok state.
    /// </summary>
    /// <param name="ok">Ok value.</param>
    /// <returns>A Result in the Ok state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOk, TError> Ok(TOk ok) => new(ok);

    /// <summary>
    /// Static constructor for the Error state.
    /// </summary>
    /// <param name="error">Error value.</param>
    /// <returns>A Result in the Error state.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOk, TError> Error(TError error) => new(error);

    /// <summary>
    /// Projects the Ok value.
    /// </summary>
    /// <param name="projection">Projection.</param>
    /// <typeparam name="TOk2">New value type.</typeparam>
    /// <returns>A new Result.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<TOk2, TError> Map<TOk2>(Func<TOk, TOk2> projection) =>
        Match(ok => projection(ok).Apply(Ok<TOk2, TError>), Error<TOk2, TError>);

    /// <summary>
    /// Projects the Ok value.
    /// <remarks>
    /// Used to enable LINQ embedded syntax, not meant for direct use.
    /// </remarks>
    /// </summary>
    /// <param name="projection">Projection.</param>
    /// <typeparam name="TOk2">New value type.</typeparam>
    /// <returns>A new Result.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<TOk2, TError> Select<TOk2>(Func<TOk, TOk2> projection) => Map(projection);

    /// <summary>
    /// Projects the error value.
    /// </summary>
    /// <param name="projection">Projection.</param>
    /// <typeparam name="TError2">New error type.</typeparam>
    /// <returns>A new Result.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<TOk, TError2> MapError<TError2>(Func<TError, TError2> projection) =>
        Match(Ok<TOk, TError2>, error => projection(error).Apply(Error<TOk, TError2>));

    /// <summary>
    /// Projects the Ok value to another async result with the same Error type and
    /// flattens the result.
    /// </summary>
    /// <param name="binder">Projection.</param>
    /// <typeparam name="TOk2">Projected type.</typeparam>
    /// <returns>A new Result.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<TOk2, TError> Bind<TOk2>(Func<TOk, Result<TOk2, TError>> binder) =>
        Map(binder).Flatten();


    /// <summary>
    /// Projects the Error value to another Result with the same Ok type and
    /// flattens the result.
    /// </summary>
    /// <param name="binder">Projection.</param>
    /// <typeparam name="TError2">Projected Error type.</typeparam>
    /// <returns>A new Result.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<TOk, TError2> BindError<TError2>(Func<TError, Result<TOk, TError2>> binder) =>
        MapError(binder).Flatten();

    /// <summary>
    /// Binds and projects the present state using a binder and a projection
    /// function.
    /// </summary>
    /// <remarks>
    /// Used to enable LINQ embedded syntax, not meant for direct use.
    /// </remarks>
    /// <param name="binder">Binding function.</param>
    /// <param name="projection">Projection.</param>
    /// <typeparam name="TOkBind">Intermediate type of the binding.</typeparam>
    /// <typeparam name="TOkFinal">Final type of the projection.</typeparam>
    /// <returns>A new Result.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<TOkFinal, TError> SelectMany<TOkBind, TOkFinal>(
        Func<TOk, Result<TOkBind, TError>> binder,
        Func<TOk, TOkBind, TOkFinal> projection
    ) =>
        Bind(ok => binder(ok).Map(okbind => projection(ok, okbind)));

    /// <summary>
    /// Collapses the structure in an output value, choosing the adequate projection
    /// for each of the states.
    /// </summary>
    /// <param name="okProjection">Value projection.</param>
    /// <param name="errorProjection">Error projection.</param>
    /// <typeparam name="TOut">Projected type.</typeparam>
    /// <returns>The correct projection output.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TOut Match<TOut>(Func<TOk, TOut> okProjection, Func<TError, TOut> errorProjection) =>
        value.Match(okProjection, errorProjection);

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fOk">Value effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Action<TOk> fOk) =>
        value.Match(ok =>
            {
                fOk(ok);
                return unit;
            },
            _ => unit);

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fOk">Value effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<TOk, Unit> fOk) =>
        Iter(ok => { fOk(ok); });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fError">Error effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Action<TError> fError) =>
        value.Match(_ => unit,
            error =>
            {
                fError(error);
                return unit;
            });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fError">Error effectful function.</param>
    /// <returns>Unit.</returns>    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<TError, Unit> fError) =>
        Iter(error => { fError(error); });

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fOk">Value effectful function.</param>
    /// <param name="fError">Error effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Func<TOk, Unit> fOk, Func<TError, Unit> fError) =>
        this.Apply(self => self.Match(fOk, fError).Apply(_ => unit));

    /// <summary>
    /// Runs an effectful function on the value for the corresponding state.
    /// </summary>
    /// <param name="fOk">Value effectful function.</param>
    /// <param name="fError">Error effectful function.</param>
    /// <returns>Unit.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Unit Iter(Action<TOk> fOk, Action<TError> fError)
    {
        Iter(fOk);
        return Iter(fError);
    }

    public Option<TOk> Option => Match(Some, _ => None);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Result<TOk, TError>(ResultOk<TOk> resultOk) => Ok(resultOk.OkValue);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Result<TOk, TError>(TOk ok) => Ok(ok);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Result<TOk, TError>(ResultError<TError> resultError) => Error(resultError.ErrorValue);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Result<TOk, TError>(TError error) => Error(error);

    public override bool Equals(object obj) =>
        obj is Result<TOk, TError> other && Equals(other);

    public bool Equals(Result<TOk, TError> other) =>
        other.value.Equals(value);

    public override int GetHashCode() =>
        -1584136870 + value.GetHashCode();
}

public readonly struct ResultOk<TOk>
{
    internal readonly TOk OkValue;

    public ResultOk(TOk okValue) =>
        OkValue = okValue;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<TOk, TError> Result<TError>() => this;
}

public readonly struct ResultError<TError>
{
    internal readonly TError ErrorValue;

    public ResultError(TError errorValue) =>
        ErrorValue = errorValue;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<TOk, TError> Result<TOk>() => this;
}

public static class ResultExtensions
{
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Collapse<TOut>(this Result<TOut, TOut> result) => result.Match(Id, Id);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncResult<TOk, TError> Async<TOk, TError>(this Task<Result<TOk, TError>> self) => self;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<TOk, TError> self) => self;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<TOk, Task<TError>> self) =>
        self.Match(ok => ok.Apply(Ok<TOk, TError>).Async(), errTsk => errTsk.Map(Error<TOk, TError>));

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<Task<TOk>, TError> self) =>
        self.Match(okTsk => okTsk.Map(Ok<TOk, TError>).Async(), error => error.Apply(Error<TOk, TError>));

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncResult<TOk, TError> Async<TOk, TError>(this Result<Task<TOk>, Task<TError>> self) =>
        self.Match(okTsk => okTsk.Map(Ok<TOk, TError>), errTsk => errTsk.Map(Error<TOk, TError>));

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AsyncResult<TOk, TError> Async<TOk, TError>(this Task<Result<Task<TOk>, Task<TError>>> self) =>
        self.Map(r => r.Async().ToTask());

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOk, TError> Flatten<TOk, TError>(this Result<TOk, Result<TOk, TError>> self) =>
        self.Match(Ok<TOk, TError>, Id);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOk, TError> Flatten<TOk, TError>(this Result<Result<TOk, TError>, TError> self) =>
        self.Match(Id, Error<TOk, TError>);
}
