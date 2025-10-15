using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource?> ElementAtOrDefaultAsync<TSource>(
          this IAsyncEnumerable<TSource> source,
          Index index,
          CancellationToken cancellationToken = default)
    {
        if (!index.IsFromEnd)
        {
            return ElementAtOrDefaultAsync(source, index.Value, cancellationToken);
        }

        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Helpers.ElementAtFromEndOrDefault(source, index.Value, throwIfNotFound: false, cancellationToken);
    }
}

file static class Helpers
{
    public static async ValueTask<TSource?> ElementAtFromEndOrDefault<TSource>(
          IAsyncEnumerable<TSource> source,
          int indexFromEnd,
          bool throwIfNotFound,
          CancellationToken cancellationToken)
    {
        if (indexFromEnd > 0)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            if (await e.MoveNextAsync())
            {
                Queue<TSource> queue = new();
                queue.Enqueue(e.Current);

                while (await e.MoveNextAsync())
                {
                    if (queue.Count == indexFromEnd)
                    {
                        queue.Dequeue();
                    }

                    queue.Enqueue(e.Current);
                }

                if (queue.Count == indexFromEnd)
                {
                    return queue.Dequeue();
                }
            }
        }

        if (throwIfNotFound)
        {
            throw new IndexOutOfRangeException("index");
        }

        return default;
    }
}
