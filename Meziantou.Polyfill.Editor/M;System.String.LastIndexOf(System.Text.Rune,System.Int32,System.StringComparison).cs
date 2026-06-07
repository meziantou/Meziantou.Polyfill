static partial class PolyfillExtensions
{
    public static int LastIndexOf(this string target, System.Text.Rune value, int startIndex, System.StringComparison comparisonType) => target.LastIndexOf(value.ToString(), startIndex, comparisonType);
}
