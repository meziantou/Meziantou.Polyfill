static partial class PolyfillExtensions
{
    public static System.Text.StringBuilder Replace(this System.Text.StringBuilder target, System.Text.Rune oldRune, System.Text.Rune newRune) => target.Replace(oldRune.ToString(), newRune.ToString());
}
