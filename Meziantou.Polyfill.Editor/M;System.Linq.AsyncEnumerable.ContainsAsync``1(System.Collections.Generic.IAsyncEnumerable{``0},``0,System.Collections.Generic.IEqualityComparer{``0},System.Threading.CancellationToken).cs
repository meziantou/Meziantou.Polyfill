using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<bool> ContainsAsync<TSource>(
           this IAsyncEnumerable<TSource> source,
           TSource value,
           IEqualityComparer<TSource>? comparer = null,
           CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken), value, comparer ?? EqualityComparer<TSource>.Default);

        async static ValueTask<bool> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source,
            TSource value,
            IEqualityComparer<TSource> comparer)
        {
            await foreach (TSource element in source)
            {
                if (comparer.Equals(element, value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
