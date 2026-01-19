// when M:System.Random.Shuffle``1(System.Span{``0})
using System;

partial class PolyfillExtensions
{
    public static void Shuffle<T>(this Random random, T[] values)
    {
        ArgumentNullException.ThrowIfNull(random);
        ArgumentNullException.ThrowIfNull(values);

        Shuffle(random, values.AsSpan());
    }
}
