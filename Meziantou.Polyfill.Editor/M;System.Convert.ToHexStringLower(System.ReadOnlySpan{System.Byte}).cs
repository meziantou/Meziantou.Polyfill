using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static string ToHexStringLower(ReadOnlySpan<byte> bytes)
        {
            if (bytes.Length == 0)
                return string.Empty;

            const string hexChars = "0123456789abcdef";
            var result = new char[bytes.Length * 2];
            
            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];
                result[i * 2] = hexChars[b >> 4];
                result[i * 2 + 1] = hexChars[b & 0xF];
            }

            return new string(result);
        }
    }
}
