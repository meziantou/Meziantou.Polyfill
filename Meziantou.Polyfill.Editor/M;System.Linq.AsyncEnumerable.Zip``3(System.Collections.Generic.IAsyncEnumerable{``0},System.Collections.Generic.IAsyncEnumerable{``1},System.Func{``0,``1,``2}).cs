using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
       this IAsyncEnumerable<TFirst> first,
       IAsyncEnumerable<TSecond> second,
       Func<TFirst, TSecond, TResult> resultSelector)
    {
        if (first is null)
            throw new ArgumentNullException(nameof(first));
        if (second is null)
            throw new ArgumentNullException(nameof(second));
        if (resultSelector is null)
            throw new ArgumentNullException(nameof(resultSelector));

        return Impl(first, second, resultSelector, default);

        static async IAsyncEnumerable<TResult> Impl(
            IAsyncEnumerable<TFirst> first,
            IAsyncEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TFirst> e1 = first.GetAsyncEnumerator(cancellationToken);
            await using IAsyncEnumerator<TSecond> e2 = second.GetAsyncEnumerator(cancellationToken);

            while (await e1.MoveNextAsync() &&
                   await e2.MoveNextAsync())
            {
                yield return resultSelector(e1.Current, e2.Current);
            }
        }
    }
}
