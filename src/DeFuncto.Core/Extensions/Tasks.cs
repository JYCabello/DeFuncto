using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DeFuncto.Extensions
{
    public static class Tasks
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<TOut> Map<TIn, TOut>(this Task<TIn> self, Func<TIn, TOut> f) =>
            f(await self);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<TOut> Map<TIn, TOut>(this Task<TIn> self, Func<TIn, Task<TOut>> f) =>
            await f(await self);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> Flatten<T>(this Task<Task<T>> self) =>
            await await self;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<List<T>> Flatten<T>(this Task<List<T>[]> self) =>
           self.Map(t => t.SelectMany(a => a)).ToList();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ToTask<T>(this T self) => self.Apply(Task.FromResult);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T[]> Parallel<T>(this IEnumerable<Func<Task<T>>> self, int maxDegreeOfParalellism = 5)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParalellism);
            return await self
                .Select(WrapInSemaphore)
                .Apply(Task.WhenAll);

            async Task<T> WrapInSemaphore(Func<Task<T>> f)
            {
                try
                {
                    await semaphore.WaitAsync();
                    return await f();
                }
                finally
                {
                    semaphore.Release();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SyncVoid<T>(this Task<T> self)
        {
            var _ = self.Result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> RunAsync<T>(this T self, Func<T, Task> f) => self.Apply(async t =>
        {
            await f(t);
            return t;
        });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> RunAsync<T>(this Task<T> self, Func<T, Task> f) => self.Map(async t =>
        {
            await f(t);
            return t;
        });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> RunAsync<T>(this T self, Func<T, Task<Unit>> f) =>
            self.RunAsync(async t => { await f(t); });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TOut>> Select<TIn, TOut>(this Task<IEnumerable<TIn>> self, Func<TIn, TOut> func) =>
            self.Map(ienumerable => ienumerable.Select(func));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TOut>> Select<TIn, TOut>(this Task<List<TIn>> self, Func<TIn, TOut> func) =>
            self.Map(ienumerable => ienumerable.Select(func));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TResult>> Select<TIn, TResult>(this Task<TIn[]> self, Func<TIn, TResult> mapper) =>
            self.Map(x => x.Select(mapper));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<List<T>> ToList<T>(this Task<IEnumerable<T>> self) =>
            self.Map(t => t.ToList());

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<List<T>> ToList<T>(this Task<T[]> self) =>
            self.Map(t => t.ToList());

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T[]> ToArray<T>(this Task<IEnumerable<T>> self) =>
            self.Map(t => t.ToArray());

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T[]> ToArray<T>(this Task<List<T>> self) =>
           self.Map(t => t.ToArray());
    }
}
