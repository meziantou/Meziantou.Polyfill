static partial class PolyfillExtensions
{
    public static bool Contains(this string target, System.Text.Rune value, System.StringComparison comparisonType) => target.IndexOf(value.ToString(), comparisonType) >= 0;
}
