static partial class PolyfillExtensions
{
    public static System.Text.StringBuilder Append(this System.Text.StringBuilder target, System.Text.Rune value) => target.Append(value.ToString());
}
