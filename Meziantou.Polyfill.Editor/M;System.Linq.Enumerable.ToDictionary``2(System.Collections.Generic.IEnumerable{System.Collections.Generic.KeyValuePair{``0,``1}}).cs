using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        where TKey : notnull
        => source.ToDictionary(comparer: null);
}
