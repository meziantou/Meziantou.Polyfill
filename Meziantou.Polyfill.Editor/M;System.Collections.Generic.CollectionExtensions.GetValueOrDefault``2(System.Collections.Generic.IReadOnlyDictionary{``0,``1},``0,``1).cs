using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        => dictionary.GetValueOrDefault(key, default!);
}
