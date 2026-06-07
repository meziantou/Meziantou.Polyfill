// when M:System.Text.RegularExpressions.Regex.EnumerateSplits(System.ReadOnlySpan{System.Char})
// when M:System.Text.RegularExpressions.Regex.EnumerateSplits(System.ReadOnlySpan{System.Char},System.Int32)
// when M:System.Text.RegularExpressions.Regex.EnumerateSplits(System.ReadOnlySpan{System.Char},System.Int32,System.Int32)
// when M:System.Text.RegularExpressions.Regex.EnumerateSplits(System.ReadOnlySpan{System.Char},System.String)
// when M:System.Text.RegularExpressions.Regex.EnumerateSplits(System.ReadOnlySpan{System.Char},System.String,System.Text.RegularExpressions.RegexOptions)
// when M:System.Text.RegularExpressions.Regex.EnumerateSplits(System.ReadOnlySpan{System.Char},System.String,System.Text.RegularExpressions.RegexOptions,System.TimeSpan)

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

internal struct ValueSplitEnumerator
{
    private readonly Range[] _ranges;
    private int _index;

    internal ValueSplitEnumerator(Regex regex, string input, int count, int startAt)
    {
        var matches = regex.Matches(input, startAt);
        var ranges = new List<Range>();
        var previous = 0;
        var remaining = count <= 0 ? int.MaxValue : count - 1;
        foreach (Match match in matches)
        {
            if (remaining-- <= 0)
                break;
            ranges.Add(new Range(previous, match.Index));
            previous = match.Index + match.Length;
        }
        ranges.Add(new Range(previous, input.Length));
        _ranges = ranges.ToArray();
        _index = -1;
    }

    public readonly Range Current => _ranges[_index];
    public readonly ValueSplitEnumerator GetEnumerator() => this;
    public bool MoveNext() => ++_index < _ranges.Length;
}
