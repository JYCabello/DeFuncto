using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DeFuncto.Extensions;

/// <summary>
/// Functions to work with "what's inside a task".
/// </summary>
public static class Tasks
{
    /// <summary>
    /// Maps the return value of a task with a synchronous function.
    /// </summary>
    /// <param name="self">Task to map.</param>
    /// <param name="f">Projection function.</param>
    /// <typeparam name="TIn">Return type in the original task.</typeparam>
    /// <typeparam name="TOut">The type of the resulting task.</typeparam>
    /// <returns>
    /// A task that will return the result of computing the original task's result with the
    /// projection function.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<TOut> Map<TIn, TOut>(this Task<TIn> self, Func<TIn, TOut> f) =>
        f(await self);

    /// <summary>
    /// Binds the output of a task to the result of an asynchronous function.
    /// </summary>
    /// <param name="self">Task to bind.</param>
    /// <param name="f">Projection function returning a Task.</param>
    /// <typeparam name="TIn">Return type in the original task.</typeparam>
    /// <typeparam name="TOut">The type of the resulting task.</typeparam>
    /// <returns>
    /// A task that will return the result of computing the original task's result with the
    /// projection function and flattening with the wrapping task.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<TOut> Map<TIn, TOut>(this Task<TIn> self, Func<TIn, Task<TOut>> f) =>
        await f(await self);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<T> Flatten<T>(this Task<Task<T>> self) =>
        await await self;

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
    public static Task<Unit> RunAsync<T>(this T self, Func<T, Task> f) => self.Apply(async t =>
    {
        await f(t);
        return Unit.Default;
    });

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<Unit> RunAsync<T>(this Task<T> self, Func<T, Task> f) => self.Map(async t =>
    {
        await f(t);
        return Unit.Default;
    });

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<Unit> RunAsync<T>(this T self, Func<T, Task<Unit>> f) =>
        self.RunAsync(async t => { await f(t); });

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<IEnumerable<TOut>> Select<TIn, TOut>(this Task<IEnumerable<TIn>> self, Func<TIn, TOut> func) =>
        self.Map(ienumerable => ienumerable.Select(func));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<IEnumerable<TOut>> Select<TIn, TOut>(this Task<List<TIn>> self, Func<TIn, TOut> func) =>
        self.Map(ienumerable => ienumerable.Select(func));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<IEnumerable<TResult>> Select<TIn, TResult>(this Task<TIn[]> self, Func<TIn, TResult> mapper) =>
        self.Map(x => x.Select(mapper));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<List<T>> ToList<T>(this Task<IEnumerable<T>> self) =>
        self.Map(t => t.ToList());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<List<T>> ToList<T>(this Task<T[]> self) =>
        self.Map(t => t.ToList());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<T[]> ToArray<T>(this Task<IEnumerable<T>> self) =>
        self.Map(t => t.ToArray());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<T[]> ToArray<T>(this Task<List<T>> self) =>
        self.Map(t => t.ToArray());
}
