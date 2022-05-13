﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using static DeFuncto.Prelude;

namespace DeFuncto.Extensions;

/// <summary>
/// Functions to work with "what's inside a task".
/// Some of them work with collections and IEnumerables inside a task.
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
    public static Task<TOut> Map<TIn, TOut>(this Task<TIn> self, Func<TIn, Task<TOut>> f) =>
        self.Map<TIn, Task<TOut>>(f).Flatten();

    /// <summary>
    /// Flattens a stacked task.
    /// </summary>
    /// <param name="self">The stacked task.</param>
    /// <typeparam name="T">The type of the value in the innermost.</typeparam>
    /// <returns> The flattened task. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<T> Flatten<T>(this Task<Task<T>> self) =>
        await await self;

    /// <summary>
    /// Flattens an array of lists inside a task.
    /// </summary>
    /// <param name="self">The task returning an array of lists.</param>
    /// <typeparam name="T">The type of the list items.</typeparam>
    /// <returns>A task with a list with the contents of the flattened arrays.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<List<T>> Flatten<T>(this Task<List<T>[]> self) =>
        self.Map(t => t.SelectMany(Id)).ToList();

    /// <summary>
    /// Wraps an object in a resolved task.
    /// </summary>
    /// <param name="self">The object to wrap.</param>
    /// <typeparam name="T">The type of the object to wrap.</typeparam>
    /// <returns>The resolved task with the object.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<T> ToTask<T>(this T self) => self.Apply(Task.FromResult);

    /// <summary>
    /// Parallelizes a collection of asynchronous functions, with a latch mechanism that
    /// limits how many can be running at the same time.
    /// </summary>
    /// <param name="self">The collection of functions.</param>
    /// <param name="maxDegreeOfParalellism">
    /// Maximum amount of functions running at the same time, defaults to 5.
    /// </param>
    /// <typeparam name="T">Type returned by the functions.</typeparam>
    /// <returns>A single task with a collection of the results.</returns>
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

    /// <summary>
    /// Synchronously resolves a task, ignoring its result.
    /// </summary>
    /// <param name="self">The task to resolve.</param>
    /// <typeparam name="T">The type of the result that will be discarded.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SyncVoid<T>(this Task<T> self)
    {
        var _ = self.Result;
    }

    /// <summary>
    /// Asynchronously applies an asynchronous action to an object.
    /// </summary>
    /// <param name="self">The object to apply the asynchronous action to.</param>
    /// <param name="f">The asynchronous action to apply.</param>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <returns>Unit, asynchronously.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<Unit> RunAsync<T>(this T self, Func<T, Task> f) =>
        self.Apply(async t =>
        {
            await f(t);
            return Unit.Default;
        });

    /// <summary>
    /// Asynchronously applies an asynchronous action to the result of a task.
    /// </summary>
    /// <param name="self">The task to apply the asynchronous action to.</param>
    /// <param name="f">The asynchronous action to apply.</param>
    /// <typeparam name="T">The type of the task's result.</typeparam>
    /// <returns>Unit, asynchronously.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<Unit> RunAsync<T>(this Task<T> self, Func<T, Task> f) =>
        self.Map(async t =>
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
