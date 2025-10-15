using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<decimal?> AverageAsync(
        this IAsyncEnumerable<decimal?> source, CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken));

        static async ValueTask<decimal?> Impl(
            ConfiguredCancelableAsyncEnumerable<decimal?> source)
        {
            decimal sum = 0;
            long count = 0;
            await foreach (decimal? item in source)
            {
                if (item is decimal value)
                {
                    sum += value;
                    count++;
                }
            }

            return count != 0 ? sum / count : null;
        }
    }
}
