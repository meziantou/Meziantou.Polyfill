// XML-DOC: M:System.Linq.AsyncEnumerable.AggregateAsync``3(System.Collections.Generic.IAsyncEnumerable{``0},``1,System.Func{``1,``0,System.Threading.CancellationToken,System.Threading.Tasks.ValueTask{``1}},System.Func{``1,System.Threading.CancellationToken,System.Threading.Tasks.ValueTask{``2}},System.Threading.CancellationToken)
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TResult> AggregateAsync<TSource, TAccumulate, TResult>(
           this IAsyncEnumerable<TSource> source,
           TAccumulate seed,
           Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> func,
           Func<TAccumulate, CancellationToken, ValueTask<TResult>> resultSelector,
           CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (func == null)
            throw new ArgumentNullException(nameof(func));
        if (resultSelector == null)
            throw new ArgumentNullException(nameof(resultSelector));

        return Impl(source, seed, func, resultSelector, cancellationToken);

        static async ValueTask<TResult> Impl(
            IAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> func,
            Func<TAccumulate, CancellationToken, ValueTask<TResult>> resultSelector,
            CancellationToken cancellationToken)
        {
            TAccumulate result = seed;

            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                result = await func(result, element, cancellationToken);
            }

            return await resultSelector(result, cancellationToken);
        }
    }
}
