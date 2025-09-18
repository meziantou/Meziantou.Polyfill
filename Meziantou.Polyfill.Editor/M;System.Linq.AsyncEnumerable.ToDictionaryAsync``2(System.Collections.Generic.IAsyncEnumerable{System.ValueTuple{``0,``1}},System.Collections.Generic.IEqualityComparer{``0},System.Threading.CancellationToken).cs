using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(
           this IAsyncEnumerable<(TKey Key, TValue Value)> source, IEqualityComparer<TKey>? comparer = null, CancellationToken cancellationToken = default) where TKey : notnull =>
           source.ToDictionaryAsync(vt => vt.Key, vt => vt.Value, comparer, cancellationToken);
}
