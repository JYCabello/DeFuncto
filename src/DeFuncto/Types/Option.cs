using System;

namespace DeFuncto
{
    public readonly struct Option<T>
    {
        private readonly T? Value;
        public bool IsSome => Value is not null;
        public bool IsNone => Value is null;

        public Option(T value) =>
            Value = value;

        public TOut Match<TOut>(Func<T, TOut> fSome, Func<TOut> fNone) =>
            IsSome ? fSome(Value!) : fNone();

        public static Option<T> Some(T value) => value;

        public static Option<T> None => new OptionNone();

        public static implicit operator Option<T>(T value) => new(value);

        public static implicit operator Option<T>(OptionNone _) => new();
    }

    public readonly struct OptionNone
    {
        public Option<T> Option<T>() => DeFuncto.Option<T>.None;
    }
}
