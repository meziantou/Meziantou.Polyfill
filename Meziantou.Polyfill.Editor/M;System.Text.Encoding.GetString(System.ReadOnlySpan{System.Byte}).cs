using System;
using System.Text;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Decodes all the bytes in the specified byte span into a string.
    /// </summary>
    /// <param name="target">The encoding used to get the string from./param>
    /// <param name="bytes">A read-only byte span to decode to a Unicode string.</param>
    /// <returns>A string that contains the decoded bytes from the provided read-only span.</returns>
    public static string GetString(this Encoding target, ReadOnlySpan<byte> bytes)
    {
#if MEZIANTOUPOLYFILL_ALLOWUNSAFE
        unsafe
        {
            fixed (byte* ptr = bytes)
            {
                return encoding.GetString(ptr, bytes.Length);
            }
        }
#else
        return target.GetString(bytes.ToArray());
#endif
    }
}