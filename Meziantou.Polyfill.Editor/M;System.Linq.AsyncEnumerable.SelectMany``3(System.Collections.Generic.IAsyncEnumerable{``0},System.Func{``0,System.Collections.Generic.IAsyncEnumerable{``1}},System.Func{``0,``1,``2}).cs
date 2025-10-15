using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>( // satisfies the C# query-expression pattern
          this IAsyncEnumerable<TSource> source,
          Func<TSource, IAsyncEnumerable<TCollection>> collectionSelector,
          Func<TSource, TCollection, TResult> resultSelector)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (collectionSelector == null)
            throw new ArgumentNullException(nameof(collectionSelector));
        if (resultSelector == null)
            throw new ArgumentNullException(nameof(resultSelector));

        return Impl(source, collectionSelector, resultSelector, default);

        async static IAsyncEnumerable<TResult> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, IAsyncEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                await foreach (TCollection subElement in collectionSelector(element).WithCancellation(cancellationToken))
                {
                    yield return resultSelector(element, subElement);
                }
            }
        }
    }
}
