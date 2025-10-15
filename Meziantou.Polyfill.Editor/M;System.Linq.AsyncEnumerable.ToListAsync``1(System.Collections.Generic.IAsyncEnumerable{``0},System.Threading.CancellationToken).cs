using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<List<TSource>> ToListAsync<TSource>(
             this IAsyncEnumerable<TSource> source,
             CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source);

        static async ValueTask<List<TSource>> Impl(
            IAsyncEnumerable<TSource> source)
        {
            List<TSource> list = [];
            await foreach (TSource element in source)
            {
                list.Add(element);
            }

            return list;
        }
    }
}
