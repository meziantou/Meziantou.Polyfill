using System;

static partial class PolyfillExtensions
{
    extension(Uri)
    {
        public static bool TryUnescapeDataString(ReadOnlySpan<char> charsToUnescape, Span<char> destination, out int charsWritten)
        {
            var unescaped = Uri.UnescapeDataString(charsToUnescape);
            if (unescaped.AsSpan().TryCopyTo(destination))
            {
                charsWritten = unescaped.Length;
                return true;
            }

            charsWritten = 0;
            return false;
        }
    }
}
