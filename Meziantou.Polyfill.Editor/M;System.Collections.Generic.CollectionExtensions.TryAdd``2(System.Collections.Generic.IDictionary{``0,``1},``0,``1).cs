#nullable enable
namespace System.Collections.Generic;

internal static partial class PolyfillExtensions
{
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        where TKey : notnull
    {
        if (dictionary is null)
            throw new ArgumentNullException(nameof(dictionary));

        if (dictionary.ContainsKey(key))
        {
            return false;
        }

        dictionary.Add(key, value);
        return true;
    }
}
