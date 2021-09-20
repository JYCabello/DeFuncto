using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DeFuncto.Extensions
{
    public static class Objects
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOut Apply<TIn, TOut>(this TIn self, Func<TIn, TOut> f) => f(self);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Unit Run<T>(this T self, Func<T, Unit> f) => self.Apply(f).Apply(_ => Unit.Default);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Unit Run<T>(this T self, Action<T> f) => self.Apply(t =>
        {
            f(t);
            return Unit.Default;
        });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Unit> Run<T>(this Task<T> self, Action<T> f) => self.Map(t =>
        {
            f(t);
            return Unit.Default;
        });
    }
}
