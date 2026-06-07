static partial class PolyfillExtensions
{
    public static string TrimEnd(this string target, System.Text.Rune trimRune)
    {
        var value = trimRune.ToString();
        var end = target.Length;
        while (end >= value.Length && target.LastIndexOf(value, end - 1, value.Length, System.StringComparison.Ordinal) == end - value.Length)
        {
            end -= value.Length;
        }

        return end == target.Length ? target : target.Substring(0, end);
    }
}
