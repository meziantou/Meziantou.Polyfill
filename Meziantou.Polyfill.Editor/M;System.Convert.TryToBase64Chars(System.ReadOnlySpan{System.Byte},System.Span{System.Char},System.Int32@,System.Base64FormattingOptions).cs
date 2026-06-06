using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static bool TryToBase64Chars(ReadOnlySpan<byte> bytes, Span<char> chars, out int charsWritten, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            var result = Convert.ToBase64String(bytes.ToArray(), options);
            if (result.Length > chars.Length)
            {
                charsWritten = 0;
                return false;
            }

            result.AsSpan().CopyTo(chars);
            charsWritten = result.Length;
            return true;
        }
    }
}
