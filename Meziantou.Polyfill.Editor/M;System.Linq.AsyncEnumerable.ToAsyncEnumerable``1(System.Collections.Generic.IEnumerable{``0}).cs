#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
using System;
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> ToAsyncEnumerable<TSource>(
          this IEnumerable<TSource> source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return source switch
        {
            TSource[] array => array.Length == 0 ? Utils.Empty<TSource>() : FromArray(array),
            List<TSource> list => FromList(list),
            IList<TSource> list => FromIList(list),
            _ when source == Enumerable.Empty<TSource>() => Utils.Empty<TSource>(),
            _ => FromIterator(source),
        };

        static async IAsyncEnumerable<TSource> FromArray(TSource[] source)
        {
            for (int i = 0; ; i++)
            {
                int localI = i;
                TSource[] localSource = source;
                if ((uint)localI >= (uint)localSource.Length)
                {
                    break;
                }
                yield return localSource[localI];
            }
        }

        static async IAsyncEnumerable<TSource> FromList(List<TSource> source)
        {
            for (int i = 0; i < source.Count; i++)
            {
                yield return source[i];
            }
        }

        static async IAsyncEnumerable<TSource> FromIList(IList<TSource> source)
        {
            int count = source.Count;
            for (int i = 0; i < count; i++)
            {
                yield return source[i];
            }
        }

        static async IAsyncEnumerable<TSource> FromIterator(IEnumerable<TSource> source)
        {
            foreach (TSource element in source)
            {
                yield return element;
            }
        }
    }


}

file static class Utils
{
    public static async IAsyncEnumerable<TSource> Empty<TSource>()
    {
        yield break;
    }
}