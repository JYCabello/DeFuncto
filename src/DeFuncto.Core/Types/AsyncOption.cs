using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.Extensions;

namespace DeFuncto
{
    public readonly struct AsyncOption<T>
    {
        private readonly Task<Option<T>> optionTask;

        public AsyncOption(Option<T> option) : this(option.Apply(Task.FromResult)) { }

        public AsyncOption(Task<Option<T>> optionTask) =>
            this.optionTask = optionTask;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TOut> Match<TOut>(Func<T, TOut> fSome, Func<TOut> fNone) =>
            optionTask.Map(option => option.Match(fSome, fNone));

        public static implicit operator AsyncOption<T>(T val) => new(val);
        public static implicit operator AsyncOption<T>(Option<T> option) => new(option);
        public static implicit operator AsyncOption<T>(Task<Option<T>> option) => new(option);
    }
}
