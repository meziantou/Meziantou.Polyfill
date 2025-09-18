using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<long> SumAsync(
          this IAsyncEnumerable<long> source,
          CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken));

        static async ValueTask<long> Impl(
            ConfiguredCancelableAsyncEnumerable<long> source)
        {
            long sum = 0;
            await foreach (long item in source)
            {
                checked { sum += item; }
            }
            return sum;
        }
    }
}
