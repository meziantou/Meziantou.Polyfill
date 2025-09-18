using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource?> LastOrDefaultAsync<TSource>(
           this IAsyncEnumerable<TSource> source,
           CancellationToken cancellationToken = default) =>
           LastOrDefaultAsync(source, default(TSource), cancellationToken);
}
