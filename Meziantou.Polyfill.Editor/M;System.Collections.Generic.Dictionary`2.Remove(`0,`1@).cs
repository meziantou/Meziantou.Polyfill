using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

static partial class PolyfillExtensions
{
    public static bool Remove<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, [MaybeNullWhen(false)] out TValue value) where TKey : notnull
    {
        var found = dictionary.TryGetValue(key, out value);
        if (found)
        {
            if (dictionary.Remove(key))
            {
#pragma warning disable CS8762 // Parameter must have a non-null value when exiting in some condition.
                return true;
#pragma warning restore CS8762 // Parameter must have a non-null value when exiting in some condition.
            }
        }

        value = default!;
        return false;
    }
}