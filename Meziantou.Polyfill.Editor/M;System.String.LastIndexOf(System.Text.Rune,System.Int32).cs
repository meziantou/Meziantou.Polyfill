static partial class PolyfillExtensions
{
    public static int LastIndexOf(this string target, System.Text.Rune value, int startIndex) => target.LastIndexOf(value.ToString(), startIndex, System.StringComparison.Ordinal);
}
