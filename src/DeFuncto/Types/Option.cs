using System;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct Option<T>
    {
        private readonly T? value;
        public readonly bool IsSome;
        public bool IsNone => !IsSome;

        public Option(T value)
        {
            this.value = value;
            IsSome = true;
        }

        public TOut Match<TOut>(Func<T, TOut> fSome, Func<TOut> fNone) =>
            IsSome ? fSome(value!) : fNone();

        public Option<TOut> Select<TOut>(Func<T, TOut> f) => Map(f);

        public Option<TOut> Map<TOut>(Func<T, TOut> f) =>
            Match(f.Compose(Option<TOut>.Some), () => Option<TOut>.None);

        public Option<TOut> Bind<TOut>(Func<T, Option<TOut>> f) =>
            Map(f).Flatten();

        public T DefaultValue(Func<T> f) =>
            Match(Id, f);

        public T DefaultValue(T t) =>
            DefaultValue(() => t);

        public Result<T, TError> Result<TError>(Func<TError> f) =>
            Map(Ok<T, TError>).DefaultValue(f().Apply(Error<T, TError>));

        public Result<T, TError> Result<TError>(TError terror) =>
            Result(() => terror);

        public static Option<T> Some(T value) => value;
        public static Option<T> None => new OptionNone();
        public static implicit operator Option<T>(T value) => new(value);
        public static implicit operator Option<T>(OptionNone _) => new();
    }

    public readonly struct OptionNone
    {
        public Option<T> Option<T>() => DeFuncto.Option<T>.None;
    }

    public static class OptionExtensions
    {
        public static Option<T> Flatten<T>(this Option<Option<T>> opt) =>
            opt.Match(Id, () => Option<T>.None);
    }
}
