using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<float> AverageAsync(
        this IAsyncEnumerable<float> source, CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken));

        static async ValueTask<float> Impl(
            ConfiguredCancelableAsyncEnumerable<float> source)
        {
            double sum = 0;
            long count = 0;
            await foreach (double item in source)
            {
                sum += item;
                count++;
            }

            if (count == 0)
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }

            return (float)(sum / count);
        }
    }
}
