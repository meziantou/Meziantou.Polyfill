using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static TKey GetKeyAtIndex<TKey, TValue>(this SortedList<TKey, TValue> sortedList, int index)
        where TKey : notnull
    {
        return sortedList.Keys[index];
    }
}
