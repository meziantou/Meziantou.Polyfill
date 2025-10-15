// XML-DOC: M:System.Linq.AsyncEnumerable.ToDictionaryAsync``2(System.Collections.Generic.IAsyncEnumerable{``0},System.Func{``0,System.Threading.CancellationToken,System.Threading.Tasks.ValueTask{``1}},System.Collections.Generic.IEqualityComparer{``1},System.Threading.CancellationToken)
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(
           this IAsyncEnumerable<TSource> source,
           Func<TSource, CancellationToken, ValueTask<TKey>> keySelector,
           IEqualityComparer<TKey>? comparer = null,
           CancellationToken cancellationToken = default) where TKey : notnull
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (keySelector is null)
            throw new ArgumentNullException(nameof(keySelector));

        return Impl(source, keySelector, comparer, cancellationToken);

        static async ValueTask<Dictionary<TKey, TSource>> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, ValueTask<TKey>> keySelector,
            IEqualityComparer<TKey>? comparer,
            CancellationToken cancellationToken)
        {
            Dictionary<TKey, TSource> d = new(comparer);
            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                d.Add(await keySelector(element, cancellationToken), element);
            }
            return d;
        }
    }
}
