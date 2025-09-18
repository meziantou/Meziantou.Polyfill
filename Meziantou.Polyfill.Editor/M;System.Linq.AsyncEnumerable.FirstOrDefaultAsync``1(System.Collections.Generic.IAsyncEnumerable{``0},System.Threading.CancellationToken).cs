using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
     public static ValueTask<TSource?> FirstOrDefaultAsync<TSource>(
            this IAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default) =>
            FirstOrDefaultAsync(source, default(TSource), cancellationToken)!;
}
