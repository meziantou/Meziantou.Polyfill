static partial class PolyfillExtensions
{
    public static bool StartsWith(this string target, System.Text.Rune value) => target.StartsWith(value.ToString(), System.StringComparison.Ordinal);
}
