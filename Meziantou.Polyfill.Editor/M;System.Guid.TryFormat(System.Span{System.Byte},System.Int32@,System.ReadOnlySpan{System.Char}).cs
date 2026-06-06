using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static bool TryFormat(this Guid target, Span<byte> destination, out int written, ReadOnlySpan<char> format = default)
    {
        var value = Encoding.UTF8.GetBytes(target.ToString(format.ToString()));
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
