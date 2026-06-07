// when M:System.Text.RegularExpressions.Regex.EnumerateMatches(System.ReadOnlySpan{System.Char})
// when M:System.Text.RegularExpressions.Regex.EnumerateMatches(System.ReadOnlySpan{System.Char},System.Int32)
// when M:System.Text.RegularExpressions.Regex.EnumerateMatches(System.ReadOnlySpan{System.Char},System.String)
// when M:System.Text.RegularExpressions.Regex.EnumerateMatches(System.ReadOnlySpan{System.Char},System.String,System.Text.RegularExpressions.RegexOptions)
// when M:System.Text.RegularExpressions.Regex.EnumerateMatches(System.ReadOnlySpan{System.Char},System.String,System.Text.RegularExpressions.RegexOptions,System.TimeSpan)

using System.Text.RegularExpressions;

internal struct ValueMatchEnumerator
{
    private readonly MatchCollection _matches;
    private int _index;

    internal ValueMatchEnumerator(Regex regex, string input, int startAt)
    {
        _matches = regex.Matches(input, startAt);
        _index = -1;
    }

    public readonly ValueMatch Current
    {
        get
        {
            var match = _matches[_index];
            return new ValueMatch(match.Index, match.Length);
        }
    }

    public readonly ValueMatchEnumerator GetEnumerator() => this;
    public bool MoveNext() => ++_index < _matches.Count;
}
