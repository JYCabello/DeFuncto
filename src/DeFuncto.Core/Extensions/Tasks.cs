﻿using System;
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
    }
}