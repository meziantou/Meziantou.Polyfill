// when M:System.Random.Shuffle``1(System.Span{``0})
using System;

partial class PolyfillExtensions
{
    public static void Shuffle<T>(this Random random, T[] values)
    {
        if (random is null)
        {
            throw new ArgumentNullException(nameof(random));
        }

        if (values is null)
        {
            throw new ArgumentNullException(nameof(values));
        }

        Shuffle(random, values.AsSpan());
    }
}
