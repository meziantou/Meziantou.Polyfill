// XML-DOC: M:System.Linq.AsyncEnumerable.SelectMany``3(System.Collections.Generic.IAsyncEnumerable{``0},System.Func{``0,System.Int32,System.Collections.Generic.IAsyncEnumerable{``1}},System.Func{``0,``1,System.Threading.CancellationToken,System.Threading.Tasks.ValueTask{``2}})
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
          this IAsyncEnumerable<TSource> source,
          Func<TSource, int, IAsyncEnumerable<TCollection>> collectionSelector,
          Func<TSource, TCollection, CancellationToken, ValueTask<TResult>> resultSelector)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (collectionSelector == null)
            throw new ArgumentNullException(nameof(collectionSelector));
        if (resultSelector == null)
            throw new ArgumentNullException(nameof(resultSelector));

        return Impl(source, collectionSelector, resultSelector, default);

        static async IAsyncEnumerable<TResult> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, int, IAsyncEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, CancellationToken, ValueTask<TResult>> resultSelector,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            int index = -1;
            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                await foreach (TCollection subElement in collectionSelector(element, checked(++index)).WithCancellation(cancellationToken))
                {
                    yield return await resultSelector(element, subElement, cancellationToken);
                }
            }
        }
    }
}
