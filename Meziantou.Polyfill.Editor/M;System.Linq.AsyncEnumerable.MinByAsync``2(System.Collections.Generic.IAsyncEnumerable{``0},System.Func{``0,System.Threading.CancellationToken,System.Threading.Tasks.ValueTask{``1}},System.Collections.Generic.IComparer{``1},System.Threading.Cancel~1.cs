// XML-DOC: M:System.Linq.AsyncEnumerable.MinByAsync``2(System.Collections.Generic.IAsyncEnumerable{``0},System.Func{``0,System.Threading.CancellationToken,System.Threading.Tasks.ValueTask{``1}},System.Collections.Generic.IComparer{``1},System.Threading.CancellationToken)
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource?> MinByAsync<TSource, TKey>(
          this IAsyncEnumerable<TSource> source,
          Func<TSource, CancellationToken, ValueTask<TKey>> keySelector,
          IComparer<TKey>? comparer = null,
          CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (keySelector is null)
            throw new ArgumentNullException(nameof(keySelector));

        return Impl(source, keySelector, comparer ?? Comparer<TKey>.Default, cancellationToken);

        static async ValueTask<TSource?> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, ValueTask<TKey>> keySelector,
            IComparer<TKey> comparer,
            CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            if (!await e.MoveNextAsync())
            {
                if (default(TSource) is not null)
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }

                return default;
            }

            TSource value = e.Current;
            TKey key = await keySelector(value, cancellationToken);

            if (default(TKey) is null)
            {
                if (key is null)
                {
                    TSource firstValue = value;

                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            // All keys are null, surface the first element.
                            return firstValue;
                        }

                        value = e.Current;
                        key = await keySelector(value, cancellationToken);
                    }
                    while (key is null);
                }

                while (await e.MoveNextAsync())
                {
                    TSource nextValue = e.Current;
                    TKey nextKey = await keySelector(nextValue, cancellationToken);
                    if (nextKey is not null && comparer.Compare(nextKey, key) < 0)
                    {
                        key = nextKey;
                        value = nextValue;
                    }
                }
            }
            else
            {
                if (comparer == Comparer<TKey>.Default)
                {
                    while (await e.MoveNextAsync())
                    {
                        TSource nextValue = e.Current;
                        TKey nextKey = await keySelector(nextValue, cancellationToken);
                        if (Comparer<TKey>.Default.Compare(nextKey, key) < 0)
                        {
                            key = nextKey;
                            value = nextValue;
                        }
                    }
                }
                else
                {
                    while (await e.MoveNextAsync())
                    {
                        TSource nextValue = e.Current;
                        TKey nextKey = await keySelector(nextValue, cancellationToken);
                        if (comparer.Compare(nextKey, key) < 0)
                        {
                            key = nextKey;
                            value = nextValue;
                        }
                    }
                }
            }

            return value;
        }
    }
}
