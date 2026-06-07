using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static void TrimExcess<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) where TKey : notnull
    {
        _ = dictionary;
    }
}
