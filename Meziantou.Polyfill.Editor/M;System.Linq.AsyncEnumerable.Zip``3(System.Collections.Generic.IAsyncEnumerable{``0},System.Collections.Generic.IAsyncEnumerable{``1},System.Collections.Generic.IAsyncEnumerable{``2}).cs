using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<(TFirst First, TSecond Second, TThird Third)> Zip<TFirst, TSecond, TThird>(
         this IAsyncEnumerable<TFirst> first,
         IAsyncEnumerable<TSecond> second,
         IAsyncEnumerable<TThird> third)
    {
        if (first is null)
            throw new ArgumentNullException(nameof(first));
        if (second is null)
            throw new ArgumentNullException(nameof(second));
        if (third is null)
            throw new ArgumentNullException(nameof(third));

        return Impl(first, second, third, default);

        static async IAsyncEnumerable<(TFirst First, TSecond Second, TThird)> Impl(
            IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, IAsyncEnumerable<TThird> third, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TFirst> e1 = first.GetAsyncEnumerator(cancellationToken);
            await using IAsyncEnumerator<TSecond> e2 = second.GetAsyncEnumerator(cancellationToken);
            await using IAsyncEnumerator<TThird> e3 = third.GetAsyncEnumerator(cancellationToken);

            while (await e1.MoveNextAsync() &&
                   await e2.MoveNextAsync() &&
                   await e3.MoveNextAsync())
            {
                yield return (e1.Current, e2.Current, e3.Current);
            }
        }
    }
}
