using System;
using System.Text;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Decodes into a span of chars a set of bytes from the specified read-only span if the destination is large enough.
    /// </summary>
    /// <param name="target">The encoding used to get the string from./param>
    /// <param name="bytes">A read-only span containing the sequence of bytes to decode.</param>
    /// <param name="chars">The character span receiving the decoded bytes.</param>
    /// <param name="charsWritten">Upon successful completion of the operation, the number of chars decoded into <paramref name="chars"/>.</param>
    /// <returns><see langword="true"/> if all of the characters were decoded into the destination; <see langword="false"/> if the destination was too small to contain all the decoded <paramref name="chars"/>.</returns>
    public static bool TryGetChars(this Encoding target, ReadOnlySpan<byte> bytes, Span<char> chars, out int charsWritten)
    {
#if MEZIANTOUPOLYFILL_ALLOWUNSAFE
        unsafe
        {
            fixed (char* ptrChars = chars)
            fixed (byte* ptrBytes = bytes)
            {
                try
                {
                    charsWritten = target.GetChars(ptrBytes, bytes.Length, ptrChars, chars.Length);
                    return true;
                }
                catch (ArgumentException)
                {
                    charsWritten = 0;
                    return false;
                }
            }
        }
#else
        var charArray = target.GetChars(bytes.ToArray(), 0, bytes.Length);

        if (charArray.Length <= chars.Length)
        {
            charsWritten = charArray.Length;
            charArray.CopyTo(chars);
            return true;
        }

        charsWritten = 0;
        return false;
#endif
    }
}