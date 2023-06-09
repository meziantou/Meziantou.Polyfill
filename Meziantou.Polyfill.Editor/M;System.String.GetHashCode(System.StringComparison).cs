using System;

static partial class PolyfillExtensions
{
    public static int GetHashCode(this string target, StringComparison comparisonType)
    {
        return Helpers.FromComparison(comparisonType).GetHashCode(target);
    }
}

file class Helpers
{
    public static StringComparer FromComparison(StringComparison comparisonType) =>
        comparisonType switch
        {
            StringComparison.CurrentCulture => StringComparer.CurrentCulture,
            StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
            StringComparison.InvariantCulture => StringComparer.InvariantCulture,
            StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
            StringComparison.Ordinal => StringComparer.Ordinal,
            StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
            _ => throw new ArgumentException("The string comparison type passed in is currently not supported.", nameof(comparisonType)),
        };
}