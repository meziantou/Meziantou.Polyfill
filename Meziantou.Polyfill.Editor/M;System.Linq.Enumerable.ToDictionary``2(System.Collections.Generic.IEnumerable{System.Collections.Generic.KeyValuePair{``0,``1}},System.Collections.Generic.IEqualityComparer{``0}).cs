using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
        this IEnumerable<KeyValuePair<TKey, TValue>> source,
        IEqualityComparer<TKey>? comparer)
            where TKey : notnull
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            => new(source, comparer);
#else
    {
        if (source is null)
        {
            throw new ArgumentNullException(paramName: nameof(source));
        }

        Dictionary<TKey, TValue> dict = new(
           capacity: (source as ICollection<KeyValuePair<TKey, TValue>>)?.Count ?? 0,
           comparer);
        foreach (var kvp in source)
        {
            dict.Add(kvp.Key, kvp.Value);
        }

        return dict;
    }
#endif
}
