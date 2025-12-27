using System;
using System.Text;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Calculates the number of bytes produced by encoding the characters in the specified string.
    /// </summary>
    /// <param name="target">The encoding used to get the byte count from./param>
    /// <param name="bytes">A read-only byte span to decode.</param>
    /// <returns>The number of characters produced by decoding the byte span.</returns>
    public static int GetCharCount(this Encoding target, ReadOnlySpan<byte> bytes)
    {
#if MEZIANTOUPOLYFILL_ALLOWUNSAFE
        unsafe
        {
            fixed (byte* ptr = bytes)
            {
                return target.GetCharCount(ptr, bytes.Length);
            }
        }
#else
        return target.GetCharCount(bytes.ToArray());
#endif
    }
}