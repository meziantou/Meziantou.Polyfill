static partial class PolyfillExtensions
{
    public static int LastIndexOf(this string target, char value, System.StringComparison comparisonType)
    {
        if (comparisonType == System.StringComparison.Ordinal) return target.LastIndexOf(value);
        return target.LastIndexOf(value.ToString(), comparisonType);
    }
}
