static partial class PolyfillExtensions
{
    public static int IndexOf(this string target, System.Text.Rune value, int startIndex) => target.IndexOf(value.ToString(), startIndex, System.StringComparison.Ordinal);
}
