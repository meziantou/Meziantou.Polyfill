static partial class PolyfillExtensions
{
    public static bool Contains(this string target, System.Text.Rune value) => target.IndexOf(value.ToString(), System.StringComparison.Ordinal) >= 0;
}
