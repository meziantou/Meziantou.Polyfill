using System;

static partial class PolyfillExtensions
{
    public static bool EndsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
    {
        if (value.Length > span.Length)
            return false;

        return span.Slice(span.Length - value.Length).ToString().Equals(value.ToString(), comparisonType);
    }
}
