using System;
using System.Collections.Generic;
static partial class PolyfillExtensions
{
    public static void InsertRange<T>(this List<T> list, int index, ReadOnlySpan<T> collection)
    {
        if (list is null)
            throw new ArgumentNullException(nameof(list));

        list.InsertRange(index, collection.ToArray());
    }
}
