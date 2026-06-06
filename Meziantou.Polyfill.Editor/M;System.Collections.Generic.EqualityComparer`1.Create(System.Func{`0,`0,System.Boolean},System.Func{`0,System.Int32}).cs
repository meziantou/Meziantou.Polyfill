using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    extension<T>(EqualityComparer<T>)
    {
        public static EqualityComparer<T> Create(Func<T?, T?, bool> equals, Func<T, int>? getHashCode = null)
        {
            if (equals is null)
                throw new ArgumentNullException(nameof(equals));

            return new DelegateEqualityComparer<T>(equals, getHashCode);
        }
    }
}

file sealed class DelegateEqualityComparer<T>(Func<T?, T?, bool> equals, Func<T, int>? getHashCode) : EqualityComparer<T>
{
    public override bool Equals(T? x, T? y) => equals(x, y);

    public override int GetHashCode(T obj) => getHashCode is null ? throw new NotSupportedException() : getHashCode(obj);
}
