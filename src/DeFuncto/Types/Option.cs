using System;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct Option<T>
    {
        private readonly T? Value;
        public readonly bool IsSome;
        public bool IsNone => !IsSome;

        public Option(T value)
        {
            Value = value;
            IsSome = true;
        }

        public TOut Match<TOut>(Func<T, TOut> fSome, Func<TOut> fNone) =>
            IsSome ? fSome(Value!) : fNone();

        public Option<TOut> Select<TOut>(Func<T, TOut> f) => Map(f);

        public Option<TOut> Map<TOut>(Func<T, TOut> f) =>
            Match(f.Compose(Option<TOut>.Some), () => Option<TOut>.None);

        public Option<TOut> Bind<TOut>(Func<T, Option<TOut>> f) =>
            Map(f).Flatten();

        public T DefaultValue(Func<T> f) =>
            Match(Id, f);
        public T DefaultValue(T t) =>
            DefaultValue(() => t);

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
