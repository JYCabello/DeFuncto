using System;

namespace DeFuncto.Extensions;

/// <summary>
/// Functions to compose nullables as if they where options.
/// </summary>
public static class Nullables
{
    /// <summary>
    /// Maps a nullable value.
    /// </summary>
    /// <param name="self">The nullable to map.</param>
    /// <param name="f">The projection.</param>
    /// <typeparam name="T">Input type.</typeparam>
    /// <typeparam name="TReturn">Projected type.</typeparam>
    /// <returns>Projected value, or null.</returns>
    public static TReturn? Map<T, TReturn>(this T? self, Func<T, TReturn> f)
        where T : struct
        where TReturn : struct =>
        self is null ? null : f(self.Value);

    /// <summary>
    /// Binds a nullable value.
    /// </summary>
    /// <param name="self">The nullable to map.</param>
    /// <param name="f">The projection to a nullable.</param>
    /// <typeparam name="T">Input type.</typeparam>
    /// <typeparam name="TReturn">Projected type.</typeparam>
    /// <returns>Projected value, or null.</returns>
    public static TReturn? Bind<T, TReturn>(this T? self, Func<T, TReturn?> f)
        where T : struct
        where TReturn : struct =>
        self is null ? null : f(self.Value);
}
