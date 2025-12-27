using System;
using System.Text;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Calculates the number of bytes produced by encoding the characters in the specified string.
    /// </summary>
    /// <param name="target">The encoding used to get the byte count from./param>
    /// <param name="chars">The span of characters to encode.</param>
    /// <returns>The number of bytes produced by encoding the specified character span.</returns>
    public static int GetByteCount(this Encoding target, ReadOnlySpan<char> chars)
    {
#if MEZIANTOUPOLYFILL_ALLOWUNSAFE
        unsafe
        {
            fixed (char* ptr = chars)
            {
                return target.GetByteCount(ptr, chars.Length);
            }
        }
#else
        return target.GetByteCount(chars.ToArray());
#endif
    }
}