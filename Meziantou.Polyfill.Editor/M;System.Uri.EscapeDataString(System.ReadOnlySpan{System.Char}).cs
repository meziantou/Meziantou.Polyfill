using System;

static partial class PolyfillExtensions
{
    extension(Uri)
    {
        public static string EscapeDataString(ReadOnlySpan<char> charsToEscape)
        {
            return Uri.EscapeDataString(charsToEscape.ToString());
        }
    }
}
