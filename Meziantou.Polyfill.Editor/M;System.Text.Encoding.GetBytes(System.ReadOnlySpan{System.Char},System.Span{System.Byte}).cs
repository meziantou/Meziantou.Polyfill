using System;
using System.Text;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Encodes into a span of bytes a set of characters from the specified read-only span.
    /// </summary>
    /// <param name="target">The encoding used to get the string from./param>
    /// <param name="chars">The span containing the set of characters to encode.</param>
    /// <param name="bytes">The byte span to hold the encoded bytes.</param>
    /// <returns>The number of encoded bytes.</returns>
    public static int GetBytes(this Encoding target, ReadOnlySpan<char> chars, Span<byte> bytes)
    {
#if MEZIANTOUPOLYFILL_ALLOWUNSAFE
        unsafe
        {
            fixed (char* ptrChars = chars)
            fixed (byte* ptrBytes = bytes)
            {
                return target.GetBytes(ptrChars, chars.Length, ptrBytes, bytes.Length);
            }
        }
#else
        return target.GetBytes(chars.ToArray(), 0, chars.Length, bytes.ToArray(), 0);
#endif
    }
}