using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DeFuncto.Extensions
{
    public static class Collections
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Choose<T>(this IEnumerable<Option<T>> self) =>
            self.Where(opt => opt.IsSome).Select(opt => opt.Value!);


        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TOut> Choose<T, TOut>(this IEnumerable<T> self, Func<T, Option<TOut>> f) =>
            self.Select(f).Choose();
    }
}
