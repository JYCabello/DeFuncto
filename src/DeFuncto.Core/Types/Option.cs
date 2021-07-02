using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
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

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TOut Match<TOut>(Func<T, TOut> fSome, Func<TOut> fNone) =>
            IsSome ? fSome(value!) : fNone();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TOut> Map<TOut>(Func<T, TOut> f) =>
            Match(f.Compose(Option<TOut>.Some), () => Option<TOut>.None);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TOut> Bind<TOut>(Func<T, Option<TOut>> f) =>
            Map(f).Flatten();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T DefaultValue(Func<T> f) =>
            Match(Id, f);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T DefaultValue(T t) =>
            DefaultValue(() => t);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, TError> Result<TError>(Func<TError> f) =>
            Map(Ok<T, TError>).DefaultValue(f.Compose(Error<T, TError>));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, TError> Result<TError>(TError terror) =>
            Result(() => terror);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TOut> Select<TOut>(Func<T, TOut> f) => Map(f);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TFinal> SelectMany<TBind, TFinal>(
            Func<T, Option<TBind>> binder,
            Func<T, TBind, TFinal> projection
        ) =>
            Bind(t => binder(t).Map(tBind => projection(t, tBind)));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Where(Func<T, bool> filter) =>
            IsSome && filter(value!) ? this : default;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Iter(Action<T> fSome, Action fNone) =>
            Match(t =>
                {
                    fSome(t);
                    return Some(t);
                },
                () =>
                {
                    fNone();
                    return None;
                });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Iter(Func<T, Unit> fSome, Func<Unit> fNone) =>
            Iter(t => { fSome(t); }, () => { fNone(); });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Iter(Func<Unit> fNone) =>
            Iter(() => { fNone(); });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Iter(Action fNone) =>
            Iter(_ => { }, fNone);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Iter(Func<T, Unit> fSome) =>
            Iter(t => { fSome(t); });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Iter(Action<T> fSome) =>
            Iter(fSome, () => { });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> Some(T value) => value;

        public static Option<T> None => new OptionNone();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Option<T>(T value) => new(value);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Option<T>(OptionNone _) => new();
    }

    public readonly struct OptionNone
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Option<T>() => DeFuncto.Option<T>.None;
    }

    public static class OptionExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> Flatten<T>(this Option<Option<T>> opt) =>
            opt.Match(Id, () => Option<T>.None);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncOption<T> Async<T>(this Option<T> opt) => opt;
    }
}
