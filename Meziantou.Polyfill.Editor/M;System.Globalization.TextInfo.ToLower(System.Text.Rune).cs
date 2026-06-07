static partial class PolyfillExtensions
{
    public static System.Text.Rune ToLower(this System.Globalization.TextInfo target, System.Text.Rune value) => System.Text.Rune.GetRuneAt(target.ToLower(value.ToString()), 0);
}
