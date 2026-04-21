using System;
using System.Buffers;

static partial class PolyfillExtensions
{
    public static int IndexOfAnyExcept<T>(this ReadOnlySpan<T> span, SearchValues<T> values) where T : IEquatable<T>?
    {
        if (values is null)
            throw new ArgumentNullException(nameof(values));

        for (var i = 0; i < span.Length; i++)
        {
            if (!values.Contains(span[i]))
                return i;
        }

        return -1;
    }
}
