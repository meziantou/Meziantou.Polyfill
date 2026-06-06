using System;

static partial class PolyfillExtensions
{
    public static bool TryFormat(this ulong target, Span<char> destination, out int written, ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        var value = target.ToString(format.ToString(), provider);
        if (value.Length > destination.Length)
        {
            written = 0;
            return false;
        }

        value.AsSpan().CopyTo(destination);
        written = value.Length;
        return true;
    }
}
