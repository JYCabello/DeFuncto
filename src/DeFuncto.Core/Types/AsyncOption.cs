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
        private readonly Task<Option<T>> optionTask;

        public Task<bool> IsSome => optionTask.Map(option => option.IsSome);
        public Task<bool> IsNone => optionTask.Map(option => option.IsNone);

        public AsyncOption(Option<T> option) : this(option.Apply(Task.FromResult)) { }

        public AsyncOption(Task<Option<T>> optionTask) =>
            this.optionTask = optionTask;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TOut> Match<TOut>(Func<T, TOut> fSome, Func<TOut> fNone) =>
            Match(fSome.Compose(Task.FromResult), fNone.Compose(Task.FromResult));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TOut> Match<TOut>(Func<T, Task<TOut>> fSome, Func<Task<TOut>> fNone) =>
            optionTask.Map(option => option.Match(fSome, fNone));

        public AsyncOption<TOut> Map<TOut>(Func<T, TOut> f) =>
            optionTask.Map(opt => opt.Map(f));

        public AsyncOption<TOut> Map<TOut>(Func<T, Task<TOut>> f) =>
            Match(
                t => f(t).Map(Some),
                () => Option<TOut>.None.Apply(Task.FromResult)
            );

        public Task<Option<T>> Option => optionTask;

        public static implicit operator AsyncOption<T>(T val) => new(val);
        public static implicit operator AsyncOption<T>(Option<T> option) => new(option);
        public static implicit operator AsyncOption<T>(Task<Option<T>> option) => new(option);
    }
}
