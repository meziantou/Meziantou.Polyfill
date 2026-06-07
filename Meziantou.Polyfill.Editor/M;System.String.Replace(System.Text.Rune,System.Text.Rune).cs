static partial class PolyfillExtensions
{
    public static string Replace(this string target, System.Text.Rune oldRune, System.Text.Rune newRune) => target.Replace(oldRune.ToString(), newRune.ToString());
}
