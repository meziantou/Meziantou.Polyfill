using System;

static partial class PolyfillExtensions
{
    public static int IndexOfAnyExcept<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
    {
        switch (values.Length)
        {
            case 0:
                return span.IsEmpty ? -1 : 0;
            case 1:
                return IndexOfAnyExcept(span, values[0]);
            case 2:
                return IndexOfAnyExcept(span, values[0], values[1]);
            case 3:
                return IndexOfAnyExcept(span, values[0], values[1], values[2]);
            default:
                for (var i = 0; i < span.Length; i++)
                {
                    if (!values.Contains(span[i])) 
                        return i;
                }

                return -1;
        }
    }
}
