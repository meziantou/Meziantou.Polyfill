using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> SkipLast<TSource>(
          this IAsyncEnumerable<TSource> source,
          int count)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return count <= 0 ? source : Helpers.TakeRangeFromEndIterator(source, isStartIndexFromEnd: false, startIndex: 0, isEndIndexFromEnd: true, endIndex: count, default);
    }
}

file static class Helpers
{
    public static async IAsyncEnumerable<TSource> TakeRangeFromEndIterator<TSource>(
         IAsyncEnumerable<TSource> source,
         bool isStartIndexFromEnd,
         int startIndex,
         bool isEndIndexFromEnd,
         int endIndex,
         [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        Debug.Assert(source is not null);
        Debug.Assert(isStartIndexFromEnd || isEndIndexFromEnd);
        Debug.Assert(isStartIndexFromEnd
            ? startIndex > 0 && (!isEndIndexFromEnd || startIndex > endIndex)
            : startIndex >= 0 && (isEndIndexFromEnd || startIndex < endIndex));

        Queue<TSource> queue;
        int count;

        if (isStartIndexFromEnd)
        {
            // TakeLast compat: enumerator should be disposed before yielding the first element.
            await using (IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken))
            {
                if (!await e.MoveNextAsync())
                {
                    yield break;
                }

                queue = new Queue<TSource>();
                queue.Enqueue(e.Current);
                count = 1;

                while (await e.MoveNextAsync())
                {
                    if (count < startIndex)
                    {
                        queue.Enqueue(e.Current);
                        ++count;
                    }
                    else
                    {
                        do
                        {
                            queue.Dequeue();
                            queue.Enqueue(e.Current);
                            checked { ++count; }
                        }
                        while (await e.MoveNextAsync());

                        break;
                    }
                }

                Debug.Assert(queue.Count == Math.Min(count, startIndex));
            }

            startIndex = CalculateStartIndexFromEnd(startIndex, count);
            endIndex = CalculateEndIndex(isEndIndexFromEnd, endIndex, count);
            Debug.Assert(endIndex - startIndex <= queue.Count);

            for (int rangeIndex = startIndex; rangeIndex < endIndex; rangeIndex++)
            {
                yield return queue.Dequeue();
            }
        }
        else
        {
            Debug.Assert(!isStartIndexFromEnd && isEndIndexFromEnd);

            // SkipLast compat: the enumerator should be disposed at the end of the enumeration.
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            count = 0;
            while (count < startIndex && await e.MoveNextAsync())
            {
                ++count;
            }

            if (count == startIndex)
            {
                queue = new Queue<TSource>();
                while (await e.MoveNextAsync())
                {
                    if (queue.Count == endIndex)
                    {
                        do
                        {
                            queue.Enqueue(e.Current);
                            yield return queue.Dequeue();
                        }
                        while (await e.MoveNextAsync());

                        break;
                    }
                    else
                    {
                        queue.Enqueue(e.Current);
                    }
                }
            }
        }

        static int CalculateStartIndexFromEnd(int startIndex, int count) =>
            Math.Max(0, count - startIndex);

        static int CalculateEndIndex(bool isEndIndexFromEnd, int endIndex, int count) =>
            Math.Min(count, isEndIndexFromEnd ? count - endIndex : endIndex);
    }
}