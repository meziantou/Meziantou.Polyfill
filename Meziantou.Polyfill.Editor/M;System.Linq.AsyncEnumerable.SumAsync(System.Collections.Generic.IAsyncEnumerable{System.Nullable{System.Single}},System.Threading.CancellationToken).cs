using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<float?> SumAsync(
          this IAsyncEnumerable<float?> source,
          CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken));

        static async ValueTask<float?> Impl(
            ConfiguredCancelableAsyncEnumerable<float?> source)
        {
            double sum = 0;
            await foreach (float? item in source)
            {
                if (item is not null)
                {
                    sum += item.GetValueOrDefault();
                }
            }
            return (float)sum;
        }
    }
}
