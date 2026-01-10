using System;

file ref struct SpanSplitEnumerator<T> where T : IEquatable<T>?
{
    private readonly ReadOnlySpan<T> _source;
    private readonly ReadOnlySpan<T> _separator;
    private readonly T _separatorValue;
    private readonly bool _useSeparatorValue;
    private int _index;
    private bool _isInitialized;

    internal SpanSplitEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> separator)
    {
        _source = source;
        _separator = separator;
        _separatorValue = default!;
        _useSeparatorValue = false;
        _index = 0;
        _isInitialized = false;
        Current = default;
    }

    internal SpanSplitEnumerator(ReadOnlySpan<T> source, T separator)
    {
        _source = source;
        _separator = default;
        _separatorValue = separator;
        _useSeparatorValue = true;
        _index = 0;
        _isInitialized = false;
        Current = default;
    }

    public SpanSplitEnumerator<T> GetEnumerator() => this;

    public bool MoveNext()
    {
        if (!_isInitialized)
        {
            _isInitialized = true;
        }

        if (_index > _source.Length)
        {
            return false;
        }

        var slice = _source.Slice(_index);
        int separatorIndex;

        if (_useSeparatorValue)
        {
            separatorIndex = slice.IndexOf(_separatorValue);
        }
        else
        {
            if (_separator.IsEmpty)
            {
                throw new ArgumentException("Separator cannot be empty.");
            }
            separatorIndex = slice.IndexOf(_separator);
        }

        if (separatorIndex < 0)
        {
            // No more separators, take the rest
            Current = new Range(_index, _source.Length);
            _index = _source.Length + 1;
            return true;
        }

        Current = new Range(_index, _index + separatorIndex);
        _index += separatorIndex + (_useSeparatorValue ? 1 : _separator.Length);
        return true;
    }

    public Range Current { get; private set; }
}

file static partial class PolyfillExtensions_Split
{
    /// <summary>
    /// Splits a span of elements into ranges based on a separator span.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="source">The span to split.</param>
    /// <param name="separator">The separator span used to delimit ranges in the source span.</param>
    /// <returns>An enumerator that iterates through the ranges in the source span.</returns>
    public static SpanSplitEnumerator<T> Split<T>(this ReadOnlySpan<T> source, ReadOnlySpan<T> separator)
        where T : IEquatable<T>?
    {
        return new SpanSplitEnumerator<T>(source, separator);
    }

    /// <summary>
    /// Splits a span of elements into ranges based on a separator element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="source">The span to split.</param>
    /// <param name="separator">The separator element used to delimit ranges in the source span.</param>
    /// <returns>An enumerator that iterates through the ranges in the source span.</returns>
    public static SpanSplitEnumerator<T> Split<T>(this ReadOnlySpan<T> source, T separator)
        where T : IEquatable<T>?
    {
        return new SpanSplitEnumerator<T>(source, separator);
    }
}

