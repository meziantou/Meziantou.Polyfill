static partial class PolyfillExtensions
{
    public static bool StartsWith(this string target, System.Text.Rune value, System.StringComparison comparisonType) => target.StartsWith(value.ToString(), comparisonType);
}
