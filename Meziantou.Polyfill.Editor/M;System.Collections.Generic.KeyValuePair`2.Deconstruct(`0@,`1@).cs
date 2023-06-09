using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> target, out TKey key, out TValue value)
    {
        key = target.Key;
        value = target.Value;
    }
}