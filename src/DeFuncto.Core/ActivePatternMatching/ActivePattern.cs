using System;
using DeFuncto.Extensions;

namespace DeFuncto.ActivePatternMatching
{
    public abstract class ActivePatternBase<TIn, TOut>
    {
        protected abstract Func<TIn, Option<object>> MatcherBase { get; }
        protected abstract Func<object, TOut> ProjectionBase { get; }
        internal bool IsMatch(TIn input) => MatcherBase(input).IsSome;

        internal TOut Get(TIn input) =>
            MatcherBase(input).Match(ProjectionBase, () => throw new ArgumentException(nameof(input)));
    }

    internal class ActivePattern<TIn, TIntermediate, TOut> : ActivePatternBase<TIn, TOut>
    {
        protected override Func<TIn, Option<object>> MatcherBase { get; }
        protected override Func<object, TOut> ProjectionBase { get; }

        internal ActivePattern(Func<TIn, Option<TIntermediate>> matcher, Func<TIntermediate, TOut> projection)
        {
            MatcherBase = tin => matcher(tin).Map<object>(it => it!);
            ProjectionBase = obj => projection((TIntermediate)obj);
        }
    }

    internal static class ActivePatternsExecutor
    {
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
}
