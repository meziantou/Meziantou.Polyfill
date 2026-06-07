// when T:System.Text.StringBuilderRuneEnumerator
static partial class PolyfillExtensions
{
    public static System.Text.StringBuilderRuneEnumerator EnumerateRunes(this System.Text.StringBuilder target) => new(target);
}
