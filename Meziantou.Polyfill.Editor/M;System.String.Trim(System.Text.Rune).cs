static partial class PolyfillExtensions
{
    public static string Trim(this string target, System.Text.Rune trimRune)
    {
        var value = trimRune.ToString();
        var start = 0;
        var end = target.Length;
        while (start + value.Length <= end && target.IndexOf(value, start, value.Length, System.StringComparison.Ordinal) == start)
        {
            start += value.Length;
        }

        while (end - value.Length >= start && target.LastIndexOf(value, end - 1, value.Length, System.StringComparison.Ordinal) == end - value.Length)
        {
            end -= value.Length;
        }

        return start == 0 && end == target.Length ? target : target.Substring(start, end - start);
    }
}
