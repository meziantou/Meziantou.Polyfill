static partial class PolyfillExtensions
{
	public static bool Contains(this string target, char value)
	{
		return target.IndexOf(value) != -1;
	}
}