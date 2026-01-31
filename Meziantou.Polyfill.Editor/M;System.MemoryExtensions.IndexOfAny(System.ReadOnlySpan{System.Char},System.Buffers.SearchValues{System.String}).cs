using System;
using System.Buffers;

static partial class PolyfillExtensions
{
    public static int IndexOfAny(this ReadOnlySpan<char> span, SearchValues<string> values)
    {
        for (var i = 0; i < span.Length; i++)
        {
            for (var j = i; j < span.Length; j++)
            {
                var substring = span.Slice(i, j - i + 1);
                if (values.Contains(substring.ToString()))
                    return i;
            }
        }

        return -1;
    }
}
