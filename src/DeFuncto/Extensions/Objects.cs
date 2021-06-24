using System;
using System.Threading.Tasks;

namespace DeFuncto.Extensions
{
    public static class Objects
    {
        public static TOut Apply<TIn, TOut>(this TIn self, Func<TIn, TOut> f) => f(self);
        public static T Run<T>(this T self, Func<T, Unit> f) => self.Apply(f).Apply(_ => self);

        public static T Run<T>(this T self, Action<T> f) => self.Apply(t =>
        {
            f(t);
            return t;
        });

        public static Task<T> RunAsync<T>(this T self, Func<T, Task> f) => self.Apply(async t =>
        {
            await f(t);
            return t;
        });

        public static Task<T> RunAsync<T>(this T self, Func<T, Task<Unit>> f) =>
            self.RunAsync(async t => { await f(t); });
    }
}
