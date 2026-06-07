static partial class PolyfillExtensions
{
    public static bool EndsWith(this string target, System.Text.Rune value, System.StringComparison comparisonType) => target.EndsWith(value.ToString(), comparisonType);
}
