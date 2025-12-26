using System;
using System.Text;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Decodes all the bytes in the specified read-only byte span into a character span.
    /// </summary>
    /// <param name="target">The encoding used to get the string from./param>
    /// <param name="bytes">A read-only span containing the sequence of bytes to decode.</param>
    /// <param name="chars">The character span receiving the decoded bytes.</param>
    /// <returns>The actual number of characters written at the span indicated by the <paramref name="chars"/> parameter.</returns>
    public static int GetChars(this Encoding target, ReadOnlySpan<byte> bytes, Span<char> chars)
    {
#if MEZIANTOUPOLYFILL_ALLOWUNSAFE
        unsafe
        {
            fixed (char* ptrChars = chars)
            fixed (byte* ptrBytes = bytes)
            {
                return target.GetChars(ptrBytes, bytes.Length, ptrChars, chars.Length);
            }
        }
#else
        var charArray = target.GetChars(bytes.ToArray(), 0, bytes.Length);
        charArray.CopyTo(chars);
        return charArray.Length;
#endif
    }
}