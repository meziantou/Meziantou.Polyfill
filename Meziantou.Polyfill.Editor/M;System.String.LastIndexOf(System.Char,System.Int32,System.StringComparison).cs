static partial class PolyfillExtensions
{
    public static int LastIndexOf(this string target, char value, int startIndex, System.StringComparison comparisonType)
    {
        if (comparisonType == System.StringComparison.Ordinal)
            return target.LastIndexOf(value, startIndex);

        return target.LastIndexOf(value.ToString(), startIndex, comparisonType);
    }
}
