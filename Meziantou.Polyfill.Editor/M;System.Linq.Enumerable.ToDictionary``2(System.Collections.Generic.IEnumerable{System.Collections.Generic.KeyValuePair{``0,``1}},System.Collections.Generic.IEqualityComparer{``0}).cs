using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
        this IEnumerable<KeyValuePair<TKey, TValue>> source,
        IEqualityComparer<TKey>? comparer)
            where TKey : notnull
    {
        if (source is null)
        {
            throw new ArgumentNullException(paramName: nameof(source));
        }

        return new(source, comparer);
    }
}
