using System;

namespace DeFuncto.Extensions;

public static class Nullables
{
    public static TReturn? Map<T, TReturn>(this T? self, Func<T, TReturn> f)
        where T : struct
        where TReturn : struct =>
        self is null
            ? null
            : f(self.Value);
}
