using System;

static partial class PolyfillExtensions
{
    extension(GC)
    {
        public static T[] AllocateUninitializedArray<T>(int length, bool pinned = false)
        {
            _ = pinned;
            return new T[length];
        }
    }
}
