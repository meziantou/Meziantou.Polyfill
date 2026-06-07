// when M:System.Text.RegularExpressions.Regex.EnumerateMatches(System.ReadOnlySpan{System.Char})
// when M:System.Text.RegularExpressions.Regex.EnumerateMatches(System.ReadOnlySpan{System.Char},System.Int32)
// when M:System.Text.RegularExpressions.Regex.EnumerateMatches(System.ReadOnlySpan{System.Char},System.String)
// when M:System.Text.RegularExpressions.Regex.EnumerateMatches(System.ReadOnlySpan{System.Char},System.String,System.Text.RegularExpressions.RegexOptions)
// when M:System.Text.RegularExpressions.Regex.EnumerateMatches(System.ReadOnlySpan{System.Char},System.String,System.Text.RegularExpressions.RegexOptions,System.TimeSpan)

internal readonly struct ValueMatch
{
    internal ValueMatch(int index, int length) => (Index, Length) = (index, length);
    public int Index { get; }
    public int Length { get; }
}
