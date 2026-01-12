#pragma warning disable IDE0290 // Use primary constructor
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
        if (_excluded is not null)
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

        if (_included is not null)
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
        if (string.IsNullOrEmpty(value))
            return null;

        List<string>? values = null;
        foreach (var part in new SemiColonSplitEnumerator(value.AsSpan()))
        {
            if (part.IsEmpty)
                continue;

            if (part.Equals("*".AsSpan(), StringComparison.Ordinal))
                return null;

            if (part.Equals("none".AsSpan(), StringComparison.OrdinalIgnoreCase))
                return [];

            values ??= [];
            values.Add(part.ToString());
        }

        if (values == null)
            return [];

        values.Sort(StringComparer.Ordinal);
        return [.. values];
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

    public string DumpAsCSharpComment()
    {
        return "// IncludedMembers: " + (_included == null ? "<null>" : string.Join(";", _included)) + "\n"
             + "// ExcludedMembers: " + (_excluded == null ? "<null>" : string.Join(";", _excluded));
    }

    [StructLayout(LayoutKind.Auto)]
    private ref struct SemiColonSplitEnumerator
    {
        private ReadOnlySpan<char> _str;

        public SemiColonSplitEnumerator(ReadOnlySpan<char> str)
        {
            _str = str;
            Current = default;
        }

        public readonly SemiColonSplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            var span = _str;
            if (span.IsEmpty)
                return false;

            var index = span.IndexOfAny(';', '|');
            if (index == -1)
            {
                _str = []; // The remaining string is an empty string
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
