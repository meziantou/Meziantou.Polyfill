using System;

static partial class PolyfillExtensions
{
    public static ReadOnlySpan<char> AsSpan(this string? text, int start, int length)
    {
        if (text == null)
        {
            if (start != 0 || length != 0)
                throw new ArgumentOutOfRangeException(nameof(start));

            return default;
        }

        return text.AsSpan().Slice(start, length);
    }
}