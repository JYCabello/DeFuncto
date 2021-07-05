using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using static DeFuncto.Prelude;

namespace DeFuncto.Extensions
{
    public static class Functions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<Unit> Unit(this Action f) =>
            () =>
            {
                f();
                return unit;
            };

        public static Action Action<T>(this Func<T> f) =>
            () => f();
    }
}
