static partial class PolyfillExtensions
{
    public static System.Text.StringBuilder Replace(this System.Text.StringBuilder target, System.Text.Rune oldRune, System.Text.Rune newRune, int startIndex, int count) => target.Replace(oldRune.ToString(), newRune.ToString(), startIndex, count);
}
