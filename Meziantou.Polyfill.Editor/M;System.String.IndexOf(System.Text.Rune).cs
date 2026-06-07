static partial class PolyfillExtensions
{
    public static int IndexOf(this string target, System.Text.Rune value) => target.IndexOf(value.ToString(), System.StringComparison.Ordinal);
}
