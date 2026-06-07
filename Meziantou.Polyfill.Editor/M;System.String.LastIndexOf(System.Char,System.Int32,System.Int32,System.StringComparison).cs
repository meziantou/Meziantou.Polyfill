static partial class PolyfillExtensions
{
    public static int LastIndexOf(this string target, char value, int startIndex, int count, System.StringComparison comparisonType)
    {
        if (comparisonType == System.StringComparison.Ordinal)
            return target.LastIndexOf(value, startIndex, count);

        return target.LastIndexOf(value.ToString(), startIndex, count, comparisonType);
    }
}
