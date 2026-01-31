using System;
using System.Buffers;

static partial class PolyfillExtensions
{
    public static int IndexOfAny(this Span<char> span, SearchValues<string> values)
    {
        return IndexOfAny((ReadOnlySpan<char>)span, values);
    }
}
