static partial class PolyfillExtensions
{
    public static int IndexOf(this string target, System.Text.Rune value, int startIndex, int count, System.StringComparison comparisonType) => target.IndexOf(value.ToString(), startIndex, count, comparisonType);
}
