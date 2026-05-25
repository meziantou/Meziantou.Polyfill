static partial class PolyfillExtensions
{
    public static int IndexOf(this string target, char value, int startIndex, int count, System.StringComparison comparisonType)
    {
        if (comparisonType == System.StringComparison.Ordinal) return target.IndexOf(value, startIndex, count);
        return target.IndexOf(value.ToString(), startIndex, count, comparisonType);
    }
}
