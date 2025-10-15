using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<double?> AverageAsync(
       this IAsyncEnumerable<int?> source, CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken));

        static async ValueTask<double?> Impl(
            ConfiguredCancelableAsyncEnumerable<int?> source)
        {
            long sum = 0;
            long count = 0;
            await foreach (int? item in source)
            {
                if (item is int value)
                {
                    checked { sum += value; }
                    count++;
                }
            }

            return count != 0 ? (double)sum / count : null;
        }
    }
}
