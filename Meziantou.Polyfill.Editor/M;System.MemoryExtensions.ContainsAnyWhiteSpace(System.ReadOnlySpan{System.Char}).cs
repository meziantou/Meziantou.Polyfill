using System;

static partial class PolyfillExtensions
{
    public static bool ContainsAnyWhiteSpace(this ReadOnlySpan<char> span)
    {
        for (var index = 0; index < span.Length; index++)
        {
            if (char.IsWhiteSpace(span[index]))
                return true;
        }

        return false;
    }
}
