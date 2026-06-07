static partial class PolyfillExtensions
{
    public static string TrimStart(this string target, System.Text.Rune trimRune)
    {
        var value = trimRune.ToString();
        var start = 0;
        while (start + value.Length <= target.Length && target.IndexOf(value, start, value.Length, System.StringComparison.Ordinal) == start)
        {
            start += value.Length;
        }

        return start == 0 ? target : target.Substring(start);
    }
}
