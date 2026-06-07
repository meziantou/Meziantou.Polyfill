using System;

static partial class PolyfillExtensions
{
    public static bool StartsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
    {
        if (value.Length > span.Length)
            return false;

        return span.Slice(0, value.Length).ToString().Equals(value.ToString(), comparisonType);
    }
}
