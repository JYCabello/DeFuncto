using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeFuncto.Extensions
{
    public static class Tasks
    {
        public static async Task<TOut> Map<TIn, TOut>(this Task<TIn> self, Func<TIn, TOut> f) =>
            f(await self);

        public static async Task<TOut> Map<TIn, TOut>(this Task<TIn> self, Func<TIn, Task<TOut>> f) =>
            await f(await self);

        public static async Task<T> Flatten<T>(this Task<Task<T>> self) =>
            await await self;

        public static Task<T[]> Parallel<T>(this IEnumerable<Func<Task<T>>> self, int maxDegreeOfParalellism = 5)
        {
            var semaphore = new SemaphoreSlim(maxDegreeOfParalellism);
            return self
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
