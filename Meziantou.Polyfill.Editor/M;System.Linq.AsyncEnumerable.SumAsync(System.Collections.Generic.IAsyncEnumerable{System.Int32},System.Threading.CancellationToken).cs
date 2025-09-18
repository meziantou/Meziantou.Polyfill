using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<int> SumAsync(
     this IAsyncEnumerable<int> source,
     CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken));

        static async ValueTask<int> Impl(
            ConfiguredCancelableAsyncEnumerable<int> source)
        {
            int sum = 0;
            await foreach (int item in source)
            {
                checked { sum += item; }
            }
            return sum;
        }
    }
}
