using DeFuncto.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using static DeFuncto.Prelude;

namespace DeFuncto.Extensions
{
    public static class Collections
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Choose<T>(this IEnumerable<Option<T>> self) =>
            self.Where(opt => opt.IsSome).Select(opt => opt.Match(Id, () => default!));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TOut> Choose<T, TOut>(this IEnumerable<T> self, Func<T, Option<TOut>> f) =>
            self.Select(f).Choose();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> FirstOrNone<T>(this IQueryable<T> query) =>
            query.Select(t => new Box<T>(t)).FirstOrDefault().Apply(Optional).Map(box => box.Value);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> self) =>
            self.Select(t => new Box<T>(t)).FirstOrDefault().Apply(Optional).Map(box => box.Value);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> FirstOrNone<T>(this List<T> self) =>
            self. Select(t => new Box<T>(t)).FirstOrDefault().Apply(Optional).Map(box => box.Value);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> FirstOrNone<T>(this IQueryable<T> query, Func<T, bool> filter) =>
            query.Where(filter).FirstOrNone();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> self, Func<T, bool> filter) =>
            self.Where(filter).FirstOrNone();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> FirstOrNone<T>(this List<T> self, Func<T, bool> filter) =>
            self.Where(filter).FirstOrNone();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> SingleOrNone<T>(this IQueryable<T> query) =>
            query.Select(t => new Box<T>(t)).SingleOrDefault().Apply(Optional).Map(box => box.Value);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> SingleOrNone<T>(this IEnumerable<T> self) =>
            self.Select(t => new Box<T>(t)).SingleOrDefault().Apply(Optional).Map(box => box.Value);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> SingleOrNone<T>(this List<T> self) =>
            self.Select(t => new Box<T>(t)).SingleOrDefault().Apply(Optional).Map(box => box.Value);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> SingleOrNone<T>(this IQueryable<T> query, Func<T, bool> filter) =>
            query.Where(filter).SingleOrNone();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> SingleOrNone<T>(this IEnumerable<T> self, Func<T, bool> filter) =>
            self.Where(filter).SingleOrNone();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> SingleOrNone<T>(this List<T> self, Func<T, bool> filter) =>
            self.Where(filter).SingleOrNone();
    }
}
