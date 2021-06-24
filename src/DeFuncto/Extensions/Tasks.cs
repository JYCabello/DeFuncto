using System;
using System.Threading.Tasks;

namespace DeFuncto.Extensions
{
    public static class Tasks
    {
        public static async Task<TOut> Map<TIn, TOut>(this Task<TIn> self, Func<TIn, TOut> f) =>
            f(await self);

        public static async Task<TOut> Map<TIn, TOut>(this Task<TIn> self, Func<TIn, Task<TOut>> f) =>
            await f(await self);
    }
}
