using System.Collections.Generic;
using System.Collections.ObjectModel;

static partial class PolyfillExtensions
{
    extension<TKey, TValue>(System.Collections.ObjectModel.ReadOnlyDictionary<TKey, TValue> target) where TKey : notnull
    {
        public static ReadOnlyDictionary<TKey, TValue> Empty
        {
            get => EmptyReadOnlyDictionary<TKey, TValue>.Instance;
        }
    }
}

file static class EmptyReadOnlyDictionary<TKey, TValue> where TKey : notnull
{
    public static readonly ReadOnlyDictionary<TKey, TValue> Instance = new(new Dictionary<TKey, TValue>());
}
