// when M:System.Uri.EscapeDataString(System.ReadOnlySpan{System.Char})
using System;

static partial class PolyfillExtensions
{
    extension(Uri)
    {
        public static bool TryEscapeDataString(ReadOnlySpan<char> charsToEscape, Span<char> destination, out int charsWritten)
        {
            var escaped = Uri.EscapeDataString(charsToEscape);
            if (escaped.AsSpan().TryCopyTo(destination))
            {
                charsWritten = escaped.Length;
                return true;
            }

            charsWritten = 0;
            return false;
        }
    }
}
