using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static DeFuncto.Prelude;

namespace DeFuncto.Extensions
{
    public static class Functions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action<T>(this Func<T> f) =>
            () => f();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<T> Action<T>(this Func<T, Unit> f) =>
            t => f(t);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T, Unit> Function<T>(this Action<T> f) =>
            t =>
            {
                f(t);
                return unit;
            };

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<Unit> Function(this Action f) =>
            () =>
            {
                f();
                return unit;
            };

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T, Task<Unit>> AsyncFunction<T>(this Func<T, Task> f) =>
           async t =>
           {
               await f(t);
               return unit;
           };

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<Task<Unit>> AsyncFunction(this Func<Task> f) =>
            async () =>
            {
                await f();
                return unit;
            };

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T, Task> AsyncAction<T>(this Func<T, Task<Unit>> f) =>
            async t => { await f(t); };

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<Task> AsyncAction(this Func<Task<Unit>> f) =>
            async () => { await f(); };
    }
}
