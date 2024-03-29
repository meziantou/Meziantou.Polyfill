using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        => dictionary.TryGetValue(key, out TValue? value) ? value : defaultValue;
}
