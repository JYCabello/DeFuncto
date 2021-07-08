using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct AsyncOption<T>
    {
        public Task<bool> IsSome => Option.Map(option => option.IsSome);
        public Task<bool> IsNone => Option.Map(option => option.IsNone);

        public AsyncOption(Option<T> option) : this(option.ToTask()) { }

        public AsyncOption(Task<Option<T>> optionTask) =>
            Option = optionTask;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TOut> Match<TOut>(Func<T, TOut> fSome, Func<TOut> fNone) =>
            Match(fSome.Compose(Task.FromResult), fNone.Compose(Task.FromResult));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TOut> Match<TOut>(Func<T, Task<TOut>> fSome, Func<Task<TOut>> fNone) =>
            Option.Map(option => option.Match(fSome, fNone));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<TOut> Map<TOut>(Func<T, TOut> f) =>
            Option.Map(opt => opt.Map(f));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<TOut> Map<TOut>(Func<T, Task<TOut>> f) =>
            Match(
                t => f(t).Map(Some),
                () => Option<TOut>.None.ToTask()
            );

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<TOut> Bind<TOut>(Func<T, AsyncOption<TOut>> f) =>
            Map(f).Flatten();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<TOut> Bind<TOut>(Func<T, Option<TOut>> f) =>
            Bind(f.Compose(OptionExtensions.Async));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<TOut> Bind<TOut>(Func<T, Task<Option<TOut>>> f) =>
            Bind(f.Compose(OptionExtensions.Async));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<T> DefaultBind(Option<T> option) =>
            DefaultBind(() => option);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<T> DefaultBind(Func<Option<T>> fOption) =>
            Match(Some, fOption);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<T> DefaultBind(Task<Option<T>> taskOption) =>
            DefaultBind(() => taskOption);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<T> DefaultBind(Func<Task<Option<T>>> fTaskOption) =>
            Match(val => Some(val).ToTask(), fTaskOption);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<T> DefaultBind(AsyncOption<T> asyncOption) =>
            DefaultBind(() => asyncOption);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<T> DefaultBind(Func<AsyncOption<T>> fAsyncOption) =>
            Match(val => Some(val).ToTask(), () => fAsyncOption().Option);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<T> DefaultValue(T t) =>
            DefaultValue(() => t);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<T> DefaultValue(Func<T> f) =>
            DefaultValue(f.Compose(Task.FromResult));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<T> DefaultValue(Task<T> task) =>
            DefaultValue(() => task);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<T> DefaultValue(Func<Task<T>> f) =>
            Match(Prelude.Compose<T, T, Task<T>>(Id, Task.FromResult), f);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<TOut> Select<TOut>(Func<T, TOut> f) => Map(f);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<TFinal> SelectMany<TBind, TFinal>(
            Func<T, AsyncOption<TBind>> binder,
            Func<T, TBind, TFinal> projection
        ) =>
            Bind(t => binder(t).Map(tBind => projection(t, tBind)));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncOption<T> Where(Func<T, bool> filter) =>
            Option.Map(opt => opt.Where(filter));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Action<T> f) =>
            Option.Run(opt => opt.Iter(f));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Func<Unit> f) =>
            Iter(f.Action());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Func<T, Unit> f) =>
            Iter(f.Action());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Func<T, Task> f) =>
            Option.Map(async option =>
            {
                await option.Match(f, () => Task.CompletedTask);
                return option;
            });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Func<T, Task<Unit>> f) =>
            Iter(f.AsyncAction());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Action f) =>
            Option.Run(opt => opt.Iter(f));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Func<Task> f) =>
            Option.Map(async opt =>
            {
                await opt.Match(_ => Task.CompletedTask, f);
                return opt;
            });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Func<Task<Unit>> f) =>
            Iter(f.AsyncAction());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Action<T> fSome, Action fNone) =>
            Iter(fSome).Async().Iter(fNone);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Func<T, Unit> fSome, Func<Unit> fNone) =>
            Iter(fSome.Action(), fNone.Action());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Func<T, Task> fSome, Func<Task> fNone) =>
            Iter(fSome).Async().Iter(fNone);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Option<T>> Iter(Func<T, Task<Unit>> fSome, Func<Task<Unit>> fNone) =>
            Iter(fSome.AsyncAction(), fNone.AsyncAction());

        public Task<Option<T>> Option { get; }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<T, TError> Result<TError>(Func<Task<TError>> fError) =>
            Match(t => Ok<T, TError>(t).ToTask(), () => fError().Map(Error<T, TError>));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<T, TError> Result<TError>(Func<TError> fError) =>
            fError.Compose(Task.FromResult).Apply(Result);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<T, TError> Result<TError>(TError error) =>
            Result(() => error);

        public static implicit operator AsyncOption<T>(T val) => new(val);
        public static implicit operator AsyncOption<T>(Option<T> option) => new(option);
        public static implicit operator AsyncOption<T>(Task<Option<T>> option) => new(option);
    }

    public static class AsyncOptionExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncOption<T> Flatten<T>(this AsyncOption<AsyncOption<T>> self) =>
            self.Match(t => t.Option, () => None.Option<T>().ToTask());
    }
}
