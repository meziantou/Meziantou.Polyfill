static partial class PolyfillExtensions
{
    public static System.Text.StringBuilder Insert(this System.Text.StringBuilder target, int index, System.Text.Rune value) => target.Insert(index, value.ToString());
}
