using System;
using System.Collections.Concurrent;

static partial class PolyfillExtensions
{
    public static TValue GetOrAdd<TKey, TValue, TArg>(this ConcurrentDictionary<TKey, TValue> target, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
    {
        return target.GetOrAdd(key, key => valueFactory(key, factoryArgument));
    }
}