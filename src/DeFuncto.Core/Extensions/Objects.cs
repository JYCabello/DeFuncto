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
        public static T Run<T>(this T self, Func<T, Unit> f) => self.Apply(f).Apply(_ => self);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Run<T>(this T self, Action<T> f) => self.Apply(t =>
        {
            f(t);
            return t;
        });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> Run<T>(this Task<T> self, Action<T> f) => self.Map(t =>
        {
            f(t);
            return t;
        });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> RunAsync<T>(this T self, Func<T, Task> f) => self.Apply(async t =>
        {
            await f(t);
            return t;
        });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> RunAsync<T>(this Task<T> self, Func<T, Task> f) => self.Map(async t =>
        {
            await f(t);
            return t;
        });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> RunAsync<T>(this T self, Func<T, Task<Unit>> f) =>
            self.RunAsync(async t => { await f(t); });
    }
}
