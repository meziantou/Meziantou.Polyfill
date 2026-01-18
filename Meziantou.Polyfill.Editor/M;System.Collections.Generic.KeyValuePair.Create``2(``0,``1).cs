using System.Collections.Generic;

static partial class PolyfillExtensions
{
    extension<TKey, TValue>(KeyValuePair<TKey, TValue>)
    {
        public static KeyValuePair<TKey, TValue> Create(TKey key, TValue value)
        {
            return new KeyValuePair<TKey, TValue>(key, value);
        }
    }
}
