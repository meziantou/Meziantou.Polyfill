using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static bool TryFormat(this DateTime target, Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
    {
        var bytes = Encoding.UTF8.GetBytes(target.ToString(format.ToString(), provider));
        if (bytes.Length > utf8Destination.Length)
        {
            bytesWritten = 0;
            return false;
        }

        bytes.AsSpan().CopyTo(utf8Destination);
        bytesWritten = bytes.Length;
        return true;
    }
}
