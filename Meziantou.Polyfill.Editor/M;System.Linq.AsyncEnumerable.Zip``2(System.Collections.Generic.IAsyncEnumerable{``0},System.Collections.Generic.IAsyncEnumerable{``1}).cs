using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(
        this IAsyncEnumerable<TFirst> first,
        IAsyncEnumerable<TSecond> second)
    {
        if (first is null)
            throw new ArgumentNullException(nameof(first));
        if (second is null)
            throw new ArgumentNullException(nameof(second));

        return Impl(first, second, default);

        static async IAsyncEnumerable<(TFirst First, TSecond Second)> Impl(
            IAsyncEnumerable<TFirst> first,
            IAsyncEnumerable<TSecond> second,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TFirst> e1 = first.GetAsyncEnumerator(cancellationToken);
            await using IAsyncEnumerator<TSecond> e2 = second.GetAsyncEnumerator(cancellationToken);

            while (await e1.MoveNextAsync() &&
                   await e2.MoveNextAsync())
            {
                yield return (e1.Current, e2.Current);
            }
        }
    }
}
