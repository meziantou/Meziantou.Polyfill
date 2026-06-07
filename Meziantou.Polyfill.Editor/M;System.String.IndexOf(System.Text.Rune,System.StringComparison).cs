static partial class PolyfillExtensions
{
    public static int IndexOf(this string target, System.Text.Rune value, System.StringComparison comparisonType) => target.IndexOf(value.ToString(), comparisonType);
}
