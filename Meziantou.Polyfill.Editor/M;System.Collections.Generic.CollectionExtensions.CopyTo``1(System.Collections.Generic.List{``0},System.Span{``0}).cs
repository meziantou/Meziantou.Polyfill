using System;
using System.Collections.Generic;
static partial class PolyfillExtensions
{
    public static void CopyTo<T>(this List<T> list, Span<T> destination)
    {
        if (list is null)
            throw new ArgumentNullException(nameof(list));

        list.ToArray().AsSpan().CopyTo(destination);
    }
}
