using System;

static partial class PolyfillExtensions
{
    public static int LastIndexOfAnyWhiteSpace(this ReadOnlySpan<char> span)
    {
        for (var index = span.Length - 1; index >= 0; index--)
        {
            if (char.IsWhiteSpace(span[index]))
                return index;
        }

        return -1;
    }
}
