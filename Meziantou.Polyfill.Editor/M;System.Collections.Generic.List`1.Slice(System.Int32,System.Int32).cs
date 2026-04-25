using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static List<T> Slice<T>(this List<T> list, int start, int length) => list.GetRange(start, length);
}
