static partial class PolyfillExtensions
{
    public static int IndexOf(this string target, System.Text.Rune value, int startIndex, System.StringComparison comparisonType) => target.IndexOf(value.ToString(), startIndex, comparisonType);
}
