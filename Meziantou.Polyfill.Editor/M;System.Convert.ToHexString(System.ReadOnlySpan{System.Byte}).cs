using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static string ToHexString(ReadOnlySpan<byte> bytes)
        {
            if (bytes.Length == 0)
                return string.Empty;

            var result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];
                result[i * 2] = HexConverter.GetHexChar(b >> 4);
                result[i * 2 + 1] = HexConverter.GetHexChar(b & 0x0F);
            }

            return new string(result);
        }
    }
}

file static class HexConverter
{
    public static char GetHexChar(int value)
    {
        return (char)(value < 10 ? '0' + value : 'A' + (value - 10));
    }
}
