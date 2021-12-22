using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DeFuncto.Extensions
{
    public static class Booleans
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOut Match<TOut>(this bool boolean, Func<TOut> onTrue, Func<TOut> onFalse) =>
            boolean ? onTrue() : onFalse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TOut> Match<TOut>(this Task<bool> boolean, Func<TOut> onTrue, Func<TOut> onFalse) =>
            boolean.Map(b => b.Match(onTrue, onFalse));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TOut> Match<TOut>(this Task<bool> boolean, Func<Task<TOut>> onTrue, Func<Task<TOut>> onFalse) =>
            boolean.Map(b => b.Match(onTrue, onFalse));
    }
}
