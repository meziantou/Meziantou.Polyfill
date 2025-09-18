using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<double?> AverageAsync(
           this IAsyncEnumerable<double?> source, CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken));

        static async ValueTask<double?> Impl(
            ConfiguredCancelableAsyncEnumerable<double?> source)
        {
            double sum = 0;
            long count = 0;
            await foreach (double? item in source)
            {
                if (item is double value)
                {
                    sum += value;
                    count++;
                }
            }

            return count != 0 ? sum / count : null;
        }
    }
}
