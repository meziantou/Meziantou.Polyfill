using System;

static partial class PolyfillExtensions
{
    public static bool Contains<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
    {
        if (default(T) != null || value is not null)
        {
            foreach (var item in span)
            {
                if (value!.Equals(item))
                    return true;
            }
        }
        else
        {
            foreach (var item in span)
            {
                if (item is null)
                    return true;
            }
        }

        return false;
    }
}