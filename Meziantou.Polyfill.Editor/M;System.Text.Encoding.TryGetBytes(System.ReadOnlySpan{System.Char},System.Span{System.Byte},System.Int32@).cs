using System;
using System.Text;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Encodes into a span of bytes a set of characters from the specified read-only span if the destination is large enough.
    /// </summary>
    /// <param name="target">The encoding used to get the string from./param>
    /// <param name="chars">The span containing the set of characters to encode.</param>
    /// <param name="bytes">The byte span to hold the encoded bytes.</param>
    /// <param name="bytesWritten">Upon successful completion of the operation, the number of bytes encoded into <paramref name="bytes"/>.</param>
    /// <returns><see langword="true"/> if all of the characters were decoded into the destination; <see langword="false"/> if the destination was too small to contain all the decoded <paramref name="bytes"/>.</returns>
    public static bool TryGetBytes(this Encoding target, ReadOnlySpan<char> chars, Span<byte> bytes, out int bytesWritten)
    {
#if MEZIANTOUPOLYFILL_ALLOWUNSAFE
        unsafe
        {
            fixed (char* ptrChars = chars)
            fixed (byte* ptrBytes = bytes)
            {
                try
                {
                    bytesWritten = target.GetBytes(ptrChars, chars.Length, ptrBytes, bytes.Length);
                    return true;
                }
                catch (ArgumentException)
                {
                    bytesWritten = 0;
                    return false;
                }
            }
        }
#else
        var byteArray = target.GetBytes(chars.ToArray(), 0, chars.Length);

        if (byteArray.Length <= bytes.Length)
        {
            bytesWritten = byteArray.Length;
            byteArray.CopyTo(bytes);
            return true;
        }

        bytesWritten = 0;
        return false;
#endif
    }
}