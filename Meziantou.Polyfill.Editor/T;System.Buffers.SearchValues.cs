using System.Collections.Generic;

namespace System.Buffers;

internal static class SearchValues
{
    public static SearchValues<T> Create<T>(ReadOnlySpan<T> values) where T : IEquatable<T>?
    {
        return new SearchValuesImpl<T>(values);
    }

    public static SearchValues<byte> Create(ReadOnlySpan<byte> values)
    {
        return new SearchValuesImpl<byte>(values);
    }

    public static SearchValues<char> Create(ReadOnlySpan<char> values)
    {
        return new SearchValuesImpl<char>(values);
    }

    public static SearchValues<string> Create(ReadOnlySpan<string> values, StringComparison comparisonType)
    {
        return new SearchValuesImplString(values, comparisonType);
    }
}

file sealed class SearchValuesImpl<T> : SearchValues<T>
    where T : IEquatable<T>?
{
    private readonly HashSet<T> _values;

    public SearchValuesImpl(ReadOnlySpan<T> values)
    {
        _values = new HashSet<T>();
        foreach (var value in values)
        {
            _values.Add(value);
        }
    }

    internal override bool Contains(T value)
    {
        return _values.Contains(value);
    }
}

file sealed class SearchValuesImplString : SearchValues<string>
{
    private readonly HashSet<string> _values;

    public SearchValuesImplString(ReadOnlySpan<string> values, StringComparison comparisonType)
    {
        var comparer = comparisonType switch
        {
            StringComparison.CurrentCulture => StringComparer.CurrentCulture,
            StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
            StringComparison.InvariantCulture => StringComparer.InvariantCulture,
            StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
            StringComparison.Ordinal => StringComparer.Ordinal,
            StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
            _ => StringComparer.Ordinal
        };

        _values = new HashSet<string>(comparer);
        foreach (var value in values)
        {
            _values.Add(value);
        }
    }

    internal override bool Contains(string value)
    {
        return _values.Contains(value);
    }
}
