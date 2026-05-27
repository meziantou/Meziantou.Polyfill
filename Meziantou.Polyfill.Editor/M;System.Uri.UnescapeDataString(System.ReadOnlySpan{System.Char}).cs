using System;

static partial class PolyfillExtensions
{
    extension(Uri)
    {
        public static string UnescapeDataString(ReadOnlySpan<char> charsToUnescape)
        {
            return Uri.UnescapeDataString(charsToUnescape.ToString());
        }
    }
}
