using System;

static partial class PolyfillExtensions
{
    public static bool TryFormat(this DateOnly target, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        var value = target.ToString(format.ToString(), provider);
        if (value.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        value.AsSpan().CopyTo(destination);
        charsWritten = value.Length;
        return true;
    }
}
