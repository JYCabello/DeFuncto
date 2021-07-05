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

        public AsyncOption(Option<T> option) : this(option.Apply(Task.FromResult)) { }

        public AsyncOption(Task<Option<T>> optionTask) =>
            this.Option = optionTask;

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
                () => Option<TOut>.None.Apply(Task.FromResult)
            );

        public Task<Option<T>> Option { get; }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncResult<T, TError> Result<TError>(Func<Task<TError>> fError) =>
            Match(t => Ok<T, TError>(t).Apply(Task.FromResult), () => fError().Map(Error<T, TError>));

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
}
