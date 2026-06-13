using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    extension<T>(EqualityComparer<T>)
    {
        public static EqualityComparer<T> Create<TKey>(Func<T?, TKey?> keySelector, IEqualityComparer<TKey>? keyComparer = null)
        {
            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            return new KeySelectorEqualityComparerPolyfill<T, TKey>(keySelector, keyComparer ?? EqualityComparer<TKey>.Default);
        }
    }
}

file sealed class KeySelectorEqualityComparerPolyfill<T, TKey>(Func<T?, TKey?> keySelector, IEqualityComparer<TKey> keyComparer) : EqualityComparer<T>
{
    public override bool Equals(T? x, T? y) => keyComparer.Equals(keySelector(x), keySelector(y));

    public override int GetHashCode(T obj)
    {
        var key = keySelector(obj);
        return key is null ? 0 : keyComparer.GetHashCode(key);
    }
}
