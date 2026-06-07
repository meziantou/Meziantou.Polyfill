using System;

static partial class PolyfillExtensions
{
    public static SpanLineEnumerator EnumerateLines(this ReadOnlySpan<char> span) => new(span);
}

internal ref struct SpanLineEnumerator
{
    private ReadOnlySpan<char> _remaining;
    private ReadOnlySpan<char> _current;

    internal SpanLineEnumerator(ReadOnlySpan<char> span) => _remaining = span;

    public readonly ReadOnlySpan<char> Current => _current;

    public readonly SpanLineEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
        if (_remaining.IsEmpty)
            return false;

        var index = _remaining.IndexOfAny('\r', '\n');
        if (index < 0)
        {
            _current = _remaining;
            _remaining = default;
            return true;
        }

        _current = _remaining.Slice(0, index);
        var newlineLength = _remaining[index] == '\r' && index + 1 < _remaining.Length && _remaining[index + 1] == '\n' ? 2 : 1;
        _remaining = _remaining.Slice(index + newlineLength);
        return true;
    }
}
