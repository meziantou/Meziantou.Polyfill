static partial class PolyfillExtensions
{
	public static bool Contains(this string target, char value, System.StringComparison comparisonType)
	{
		return target.IndexOf(value, comparisonType) != -1;
	}
}