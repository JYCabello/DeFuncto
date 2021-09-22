using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DeFuncto.Extensions;
using static DeFuncto.Prelude;

namespace DeFuncto
{
    public readonly struct Option<T> : IEquatable<Option<T>>
    {
        private readonly Du<Unit, T> value;
        public readonly bool IsSome;
        public bool IsNone => !IsSome;

        public Option(T value)
        {
            this.value = value;
            IsSome = true;
        }

        private Option(Unit none)
        {
            value = none;
            IsSome = false;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TOut Match<TOut>(Func<T, TOut> fSome, Func<TOut> fNone) =>
            value.Match(_ => fNone(), fSome);

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
        public Option<T> BindNone(Option<T> opt) =>
            BindNone(() => opt);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> BindNone(Func<Option<T>> opt) =>
            Match(Some, opt);

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
            this.Apply(self => self.Match(val => filter(val) ? self : None, () => default));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T> fSome, Action fNone) =>
            Match(t =>
                {
                    fSome(t);
                    return unit;
                },
                () =>
                {
                    fNone();
                    return unit;
                });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T, Unit> fSome, Func<Unit> fNone) =>
            Iter(t => { fSome(t); }, () => { fNone(); });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<Unit> fNone) =>
            Iter(() => { fNone(); });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action fNone) =>
            Iter(_ => { }, fNone);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Func<T, Unit> fSome) =>
            Iter(t => { fSome(t); });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Iter(Action<T> fSome) =>
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
        public static implicit operator Option<T>(OptionNone _) => new(unit);

        public override bool Equals(object obj) =>
            obj is Option<T> other && Equals(other);

        public bool Equals(Option<T> other) =>
            IsSome == other.IsSome && other.value.Equals(value);

        public override int GetHashCode() =>
            -1584136870 + value.GetHashCode();
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

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncOption<T> Async<T>(this Task<Option<T>> opt) => opt;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncOption<T> Async<T>(this Option<Task<T>> opt) =>
            opt.Match(t => t.Map(Some), () => None.Option<T>().ToTask());
    }
}
