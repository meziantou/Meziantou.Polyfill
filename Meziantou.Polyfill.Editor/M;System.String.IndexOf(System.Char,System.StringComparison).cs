static partial class PolyfillExtensions
{
    public static int IndexOf(this string target, char value, System.StringComparison comparisonType)
    {
        if (comparisonType == System.StringComparison.Ordinal) return target.IndexOf(value);
        return target.IndexOf(value.ToString(), comparisonType);
    }
}