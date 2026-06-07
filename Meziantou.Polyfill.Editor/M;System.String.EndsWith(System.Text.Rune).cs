static partial class PolyfillExtensions
{
    public static bool EndsWith(this string target, System.Text.Rune value) => target.EndsWith(value.ToString(), System.StringComparison.Ordinal);
}
