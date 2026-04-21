using System;

static partial class PolyfillExtensions
{
    extension(string)
    {
        public static int GetHashCode(ReadOnlySpan<char> value, StringComparison comparisonType)
        {
            return value.ToString().GetHashCode(comparisonType);
        }
    }
}
