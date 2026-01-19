#nullable enable
namespace System.Collections.Generic;

using System.Diagnostics.CodeAnalysis;

internal static partial class PolyfillExtensions
{
    public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, [MaybeNullWhen(false)] out TValue value)
        where TKey : notnull
    {
        if (dictionary is null)
            throw new ArgumentNullException(nameof(dictionary));

        if (dictionary.TryGetValue(key, out value))
        {
            dictionary.Remove(key);
            return true;
        }

        value = default!;
        return false;
    }
}
