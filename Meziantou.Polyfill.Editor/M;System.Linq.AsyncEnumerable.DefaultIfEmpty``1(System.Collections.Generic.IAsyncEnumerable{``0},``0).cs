using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> DefaultIfEmpty<TSource>(
          this IAsyncEnumerable<TSource> source, TSource defaultValue)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source, defaultValue, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            TSource defaultValue,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            if (await e.MoveNextAsync())
            {
                do
                {
                    yield return e.Current;
                }
                while (await e.MoveNextAsync());
            }
            else
            {
                yield return defaultValue;
            }
        }
    }
}
