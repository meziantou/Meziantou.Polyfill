using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<TSource> Reverse<TSource>(this TSource[] array) =>
        System.Linq.Enumerable.Reverse<TSource>(array);
}
