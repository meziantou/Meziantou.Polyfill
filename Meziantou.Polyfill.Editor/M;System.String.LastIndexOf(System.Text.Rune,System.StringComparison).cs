static partial class PolyfillExtensions
{
    public static int LastIndexOf(this string target, System.Text.Rune value, System.StringComparison comparisonType) => target.LastIndexOf(value.ToString(), comparisonType);
}
