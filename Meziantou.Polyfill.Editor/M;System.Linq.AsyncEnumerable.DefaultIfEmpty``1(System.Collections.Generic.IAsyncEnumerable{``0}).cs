using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource?> DefaultIfEmpty<TSource>(
           this IAsyncEnumerable<TSource> source) =>
           DefaultIfEmpty(source, default);
}
