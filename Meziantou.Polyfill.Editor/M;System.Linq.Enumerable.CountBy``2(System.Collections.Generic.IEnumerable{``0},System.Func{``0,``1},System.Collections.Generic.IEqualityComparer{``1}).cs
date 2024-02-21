using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? keyComparer = null) where TKey : notnull
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (keySelector is null)
        {
            throw new ArgumentNullException(nameof(keySelector));
        }

        return Helpers.CountByIterator(source, keySelector, keyComparer);
    }
}

file class Helpers
{
    public static IEnumerable<KeyValuePair<TKey, int>> CountByIterator<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? keyComparer) where TKey : notnull
    {
        using IEnumerator<TSource> enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            yield break;
        }

        foreach (KeyValuePair<TKey, int> countBy in BuildCountDictionary(enumerator, keySelector, keyComparer))
        {
            yield return countBy;
        }
    }

    public static Dictionary<TKey, int> BuildCountDictionary<TSource, TKey>(IEnumerator<TSource> enumerator, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? keyComparer) where TKey : notnull
    {
        Dictionary<TKey, int> countsBy = new(keyComparer);

        do
        {
            TSource value = enumerator.Current;
            TKey key = keySelector(value);

#if NET
            ref int currentCount = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(countsBy, key, out _);
            checked
            {
                currentCount++;
            }
#else
            if (countsBy.TryGetValue(key, out var currentCount))
            {
                checked
                {
                    countsBy[key] = currentCount + 1;
                }
            }
            else
            {
                countsBy[key] = 1;
            }
#endif
        }
        while (enumerator.MoveNext());

        return countsBy;
    }
}