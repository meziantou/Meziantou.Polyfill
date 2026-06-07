static partial class PolyfillExtensions
{
    public static int LastIndexOf(this string target, System.Text.Rune value, int startIndex, int count) => target.LastIndexOf(value.ToString(), startIndex, count, System.StringComparison.Ordinal);
}
