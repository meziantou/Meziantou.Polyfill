using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource> ElementAtAsync<TSource>(
           this IAsyncEnumerable<TSource> source,
           int index,
           CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return Helpers.ElementAtOrDefaultAsync(source, index, throwIfNotFound: true, cancellationToken)!;
    }
}

file static class Helpers
{
    public static async ValueTask<TSource?> ElementAtOrDefaultAsync<TSource>(
          IAsyncEnumerable<TSource> source,
          int index,
          bool throwIfNotFound,
          CancellationToken cancellationToken = default)
    {
        if (index >= 0)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            while (await e.MoveNextAsync())
            {
                if (index == 0)
                {
                    return e.Current;
                }

                index--;
            }
        }

        if (throwIfNotFound)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return default;
    }
}