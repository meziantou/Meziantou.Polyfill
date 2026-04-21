using System;
using System.Buffers;

static partial class PolyfillExtensions
{
    public static int IndexOfAny(this ReadOnlySpan<char> span, SearchValues<string> values)
    {
        if (values is null)
            throw new ArgumentNullException(nameof(values));

        if (values.Contains(string.Empty))
            return 0;

        for (var start = 0; start < span.Length; start++)
        {
            var remainingLength = span.Length - start;
            for (var length = 1; length <= remainingLength; length++)
            {
                if (values.Contains(span.Slice(start, length).ToString()))
                    return start;
            }
        }

        return -1;
    }
}
