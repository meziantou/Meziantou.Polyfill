using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource[]> ToArrayAsync<TSource>(
          this IAsyncEnumerable<TSource> source,
          CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken));

        static async ValueTask<TSource[]> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source)
        {
            await using ConfiguredCancelableAsyncEnumerable<TSource>.Enumerator e = source.GetAsyncEnumerator();

            if (await e.MoveNextAsync())
            {
                List<TSource> list = [];
                do
                {
                    list.Add(e.Current);
                }
                while (await e.MoveNextAsync());

                return list.ToArray();
            }

            return [];
        }
    }
}
