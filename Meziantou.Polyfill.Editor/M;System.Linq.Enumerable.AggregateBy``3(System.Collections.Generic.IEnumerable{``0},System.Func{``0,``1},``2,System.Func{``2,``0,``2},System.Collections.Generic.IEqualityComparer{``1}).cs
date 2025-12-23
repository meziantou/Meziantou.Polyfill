using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
	public static IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector,
		TAccumulate seed,
		Func<TAccumulate, TSource, TAccumulate> func,
		IEqualityComparer<TKey>? keyComparer = null) where TKey : notnull
	{
		if (source is null)
		{
            throw new ArgumentNullException(nameof(source));
		}
		if (keySelector is null)
		{
            throw new ArgumentNullException(nameof(keySelector));
		}
		if (func is null)
		{
            throw new ArgumentNullException(nameof(func));
		}

		return Helpers.AggregateByIterator(source, keySelector, seed, func, keyComparer);
	}
}

file class Helpers
{
	public static IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateByIterator<TSource, TKey, TAccumulate>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, IEqualityComparer<TKey>? keyComparer) where TKey : notnull
	{
		using IEnumerator<TSource> enumerator = source.GetEnumerator();

		if (!enumerator.MoveNext())
		{
			yield break;
		}

		foreach (KeyValuePair<TKey, TAccumulate> countBy in PopulateDictionary(enumerator, keySelector, seed, func, keyComparer))
		{
			yield return countBy;
		}

		static Dictionary<TKey, TAccumulate> PopulateDictionary(IEnumerator<TSource> enumerator, Func<TSource, TKey> keySelector, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, IEqualityComparer<TKey>? keyComparer)
		{
			Dictionary<TKey, TAccumulate> dict = new(keyComparer);

			do
			{
				TSource value = enumerator.Current;
				TKey key = keySelector(value);
								
#if NET6_0_OR_GREATER
				ref TAccumulate? acc = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(dict, key, out bool exists);
				acc = func(exists ? acc! : seed, value);
#else
				var exists = dict.TryGetValue(key, out var acc);
				dict[key] = func(exists ? acc! : seed, value);
#endif
			}
			while (enumerator.MoveNext());

			return dict;
		}
	} 
}