static partial class PolyfillExtensions
{
    public static string[] Split(this string target, System.Text.Rune separator, System.StringSplitOptions options = System.StringSplitOptions.None) => target.Split(new[] { separator.ToString() }, options);
}
