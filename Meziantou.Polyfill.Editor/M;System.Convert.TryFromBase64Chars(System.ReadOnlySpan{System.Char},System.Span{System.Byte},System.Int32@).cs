using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static bool TryFromBase64Chars(ReadOnlySpan<char> chars, Span<byte> bytes, out int bytesWritten)
        {
            try
            {
                var result = Convert.FromBase64String(chars.ToString());
                if (result.Length > bytes.Length)
                {
                    bytesWritten = 0;
                    return false;
                }

                result.AsSpan().CopyTo(bytes);
                bytesWritten = result.Length;
                return true;
            }
            catch (FormatException)
            {
                bytesWritten = 0;
                return false;
            }
        }
    }
}
