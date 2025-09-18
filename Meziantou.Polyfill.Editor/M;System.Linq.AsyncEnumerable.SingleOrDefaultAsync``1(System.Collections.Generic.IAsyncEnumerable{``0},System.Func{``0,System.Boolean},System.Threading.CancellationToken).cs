using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource?> SingleOrDefaultAsync<TSource>(
     this IAsyncEnumerable<TSource> source,
     Func<TSource, bool> predicate,
     CancellationToken cancellationToken = default) =>
        SingleOrDefaultAsync(source, predicate!, default, cancellationToken);
}
