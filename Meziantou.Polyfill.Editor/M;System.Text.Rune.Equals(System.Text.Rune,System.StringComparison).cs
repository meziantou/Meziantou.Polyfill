static partial class PolyfillExtensions
{
    public static bool Equals(this System.Text.Rune target, System.Text.Rune other, System.StringComparison comparisonType) => string.Equals(target.ToString(), other.ToString(), comparisonType);
}
