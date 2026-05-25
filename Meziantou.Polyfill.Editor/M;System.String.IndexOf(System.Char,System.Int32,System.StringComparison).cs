static partial class PolyfillExtensions
{
    public static int IndexOf(this string target, char value, int startIndex, System.StringComparison comparisonType)
    {
        if (comparisonType == System.StringComparison.Ordinal) return target.IndexOf(value, startIndex);
        return target.IndexOf(value.ToString(), startIndex, comparisonType);
    }
}
