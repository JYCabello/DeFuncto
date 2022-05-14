using System;
using DeFuncto.Extensions;

namespace DeFuncto.ActivePatternMatching;

/// <summary>
/// Base class for the active pattern matching.
/// </summary>
/// <remarks>
/// This bit is not, and never will, be documented on the official documentation.
/// If you reach this point, this makes sense to you and you want to use it, it's time
/// to make a move to F# or to step back in your usage of functional programming.
/// You are on the edge of what I believe C# *should* do for you when it comes to FP.
/// </remarks>
/// <typeparam name="TIn">Input type to match on.</typeparam>
/// <typeparam name="TOut">Output type of the matching.</typeparam>
public abstract class ActivePatternBase<TIn, TOut>
{
    /// <summary>
    /// Matching function.
    /// </summary>
    protected abstract Func<TIn, Option<object>> MatcherBase { get; }

    /// <summary>
    /// Projection function.
    /// </summary>
    protected abstract Func<object, TOut> ProjectionBase { get; }

    /// <summary>
    /// Determines if the pattern matches for the input.
    /// </summary>
    /// <param name="input">Input value.</param>
    /// <returns>True if there was a match.</returns>
    internal bool IsMatch(TIn input) => MatcherBase(input).IsSome;

    /// <summary>
    /// Gets the projected value, unsafe.
    /// </summary>
    /// <param name="input">Input value.</param>
    /// <returns>Projection of the input value.</returns>
    /// <exception cref="ArgumentException">Called when not matching.</exception>
    internal TOut Get(TIn input) =>
        MatcherBase(input).Match(ProjectionBase, () => throw new ArgumentException(nameof(input)));
}

/// <summary>
/// Active pattern implementation.
/// </summary>
/// <remarks>
/// This bit is not, and never will, be documented on the official documentation.
/// If you reach this point, this makes sense to you and you want to use it, it's time
/// to make a move to F# or to step back in your usage of functional programming.
/// You are on the edge of what I believe C# *should* do for you when it comes to FP.
/// </remarks>
/// <typeparam name="TIn">Input type to match on.</typeparam>
/// <typeparam name="TIntermediate">Intermediate type for when a match happens.</typeparam>
/// <typeparam name="TOut">Projection on the matched value.</typeparam>
public class ActivePattern<TIn, TIntermediate, TOut> : ActivePatternBase<TIn, TOut>
{
    /// <summary>
    /// Default constructor for pattern matcher.
    /// </summary>
    /// <param name="matcher">Matching function.</param>
    /// <param name="projection">Projection function.</param>
    internal ActivePattern(Func<TIn, Option<TIntermediate>> matcher, Func<TIntermediate, TOut> projection)
    {
        MatcherBase = tin => matcher(tin).Map<object>(it => it!);
        ProjectionBase = obj => projection((TIntermediate)obj);
    }

    /// <summary>
    /// Matching function.
    /// </summary>
    protected override Func<TIn, Option<object>> MatcherBase { get; }

    /// <summary>
    /// Projection.
    /// </summary>
    protected override Func<object, TOut> ProjectionBase { get; }
}

/// <summary>
/// Function to execute active pattern matching.
/// </summary>
/// <remarks>
/// This bit is not, and never will, be documented on the official documentation.
/// If you reach this point, this makes sense to you and you want to use it, it's time
/// to make a move to F# or to step back in your usage of functional programming.
/// You are on the edge of what I believe C# *should* do for you when it comes to FP.
/// </remarks>
internal static class ActivePatternsExecutor
{
    /// <summary>
    /// Execute pattern matching.
    /// </summary>
    /// <param name="input">Input value.</param>
    /// <param name="patterns">Patterns to use, in order.</param>
    /// <param name="defaulter">Default projection.</param>
    /// <typeparam name="TIn">Input type.</typeparam>
    /// <typeparam name="TOut">Output type.</typeparam>
    /// <returns>The match result.</returns>
    internal static TOut Execute<TIn, TOut>(
        TIn input,
        ActivePatternBase<TIn, TOut>[] patterns,
        Func<TIn, TOut> defaulter
    ) =>
        patterns
            .FirstOrNone(p => p.IsMatch(input))
            .Match(
                p => p.Get(input),
                () => defaulter(input)
            );
}
