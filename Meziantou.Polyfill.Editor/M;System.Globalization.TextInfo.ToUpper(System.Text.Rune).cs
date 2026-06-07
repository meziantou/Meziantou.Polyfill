static partial class PolyfillExtensions
{
    public static System.Text.Rune ToUpper(this System.Globalization.TextInfo target, System.Text.Rune value) => System.Text.Rune.GetRuneAt(target.ToUpper(value.ToString()), 0);
}
