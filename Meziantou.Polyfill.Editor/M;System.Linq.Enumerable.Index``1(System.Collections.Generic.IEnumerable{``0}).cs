using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<(int Index, TSource Item)> Index<TSource>(this IEnumerable<TSource> source)
    {
        int index = -1;
        foreach (TSource element in source)
        {
            checked
            {
                index++;
            }

            yield return (index, element);
        }
    }
}