using System;

static partial class PolyfillExtensions
{
    public static bool Equals(this char target, char other, StringComparison comparisonType) => string.Equals(target.ToString(), other.ToString(), comparisonType);
}
