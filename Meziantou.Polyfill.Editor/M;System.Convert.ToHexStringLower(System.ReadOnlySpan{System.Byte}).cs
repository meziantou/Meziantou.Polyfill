using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static string ToHexStringLower(ReadOnlySpan<byte> bytes)
        {
            if (bytes.Length == 0)
                return string.Empty;

            const int AddToAlpha = 87;
            const int AddToDigit = -39;

            var c = new char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i] >> 4;
                c[i * 2] = (char)(AddToAlpha + b + (((b - 10) >> 31) & AddToDigit));

                b = bytes[i] & 0xF;
                c[(i * 2) + 1] = (char)(AddToAlpha + b + (((b - 10) >> 31) & AddToDigit));
            }

            return new string(c);
        }
    }
}
