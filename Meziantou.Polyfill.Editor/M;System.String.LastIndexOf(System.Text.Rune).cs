static partial class PolyfillExtensions
{
    public static int LastIndexOf(this string target, System.Text.Rune value) => target.LastIndexOf(value.ToString(), System.StringComparison.Ordinal);
}
