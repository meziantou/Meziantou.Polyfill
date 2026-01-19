using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static TValue GetValueAtIndex<TKey, TValue>(this SortedList<TKey, TValue> sortedList, int index)
        where TKey : notnull
    {
        return sortedList.Values[index];
    }
}
