using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Meziantou.Polyfill;

internal sealed class PolyfillOptions : IEquatable<PolyfillOptions>
{
    private readonly string[]? _included;
    private readonly string[]? _excluded;

    public PolyfillOptions(string? included, string? excluded)
    {
        _included = ParseValues(included);
        _excluded = ParseValues(excluded);
    }

    public bool Include(string memberDocumentationId)
    {
        if (_excluded != null)
        {
            var found = false;
            foreach (var filter in _excluded)
            {
                if (memberDocumentationId.StartsWith(filter, StringComparison.Ordinal))
                {
                    found = true;
                    break;
                }
            }

            if (found)
                return false;
        }

        if (_included != null)
        {
            var found = false;
            foreach (var filter in _included)
            {
                if (memberDocumentationId.StartsWith(filter, StringComparison.Ordinal))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                return false;
        }

        return true;
    }

    private static string[]? ParseValues(string? value)
    {
        if (value == null)
            return null;

        List<string>? values = null;
        foreach (var part in new LineSplitEnumerator(value.AsSpan()))
        {
            if (part.IsEmpty)
                continue;

            if (part.Equals("*".AsSpan(), StringComparison.Ordinal))
                return null;

            values ??= new List<string>();
            values.Add(part.ToString());
        }

        if (values == null)
            return Array.Empty<string>();

        values.Sort(StringComparer.Ordinal);
        return values.ToArray();
    }

    public override int GetHashCode() => 0;
    public override bool Equals(object obj) => Equals(obj as PolyfillOptions);
    public bool Equals(PolyfillOptions? other)
    {
        if (other == null)
            return false;

        return SequenceEqual(_included, other._included)
            && SequenceEqual(_excluded, other._excluded);

        static bool SequenceEqual(string[]? value1, string[]? value2)
        {
            if (value1 == value2)
                return true;

            if (value1 == null || value2 == null)
                return false;

            return value1.SequenceEqual(value2, StringComparer.Ordinal);
        }
    }

    [StructLayout(LayoutKind.Auto)]
    private ref struct LineSplitEnumerator
    {
        private ReadOnlySpan<char> _str;

        public LineSplitEnumerator(ReadOnlySpan<char> str)
        {
            _str = str;
            Current = default;
        }

        public readonly LineSplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            var span = _str;
            if (span.IsEmpty)
                return false;

            var index = span.IndexOf(';');
            if (index == -1)
            {
                _str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
                Current = span.Trim();
                return true;
            }

            Current = span.Slice(0, index).Trim();
            _str = span.Slice(index + 1);
            return true;
        }

        public ReadOnlySpan<char> Current { get; private set; }
    }
}
