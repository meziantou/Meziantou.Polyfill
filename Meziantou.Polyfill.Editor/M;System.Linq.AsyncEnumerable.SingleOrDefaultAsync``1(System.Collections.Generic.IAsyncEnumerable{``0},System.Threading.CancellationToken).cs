using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource?> SingleOrDefaultAsync<TSource>(
    this IAsyncEnumerable<TSource> source,
    CancellationToken cancellationToken = default) =>
        SingleOrDefaultAsync(source, default(TSource), cancellationToken);
}
