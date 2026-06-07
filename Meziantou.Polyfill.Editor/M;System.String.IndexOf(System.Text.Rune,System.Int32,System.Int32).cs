static partial class PolyfillExtensions
{
    public static int IndexOf(this string target, System.Text.Rune value, int startIndex, int count) => target.IndexOf(value.ToString(), startIndex, count, System.StringComparison.Ordinal);
}
