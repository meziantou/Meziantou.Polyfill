using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static bool TryToHexString(ReadOnlySpan<byte> source, Span<byte> utf8Destination, out int bytesWritten)
        {
            if (source.Length > int.MaxValue / 2 || utf8Destination.Length < source.Length * 2)
            {
                bytesWritten = 0;
                return false;
            }

            for (var i = 0; i < source.Length; i++)
            {
                utf8Destination[i * 2] = GetUpperHexByte(source[i] >> 4);
                utf8Destination[(i * 2) + 1] = GetUpperHexByte(source[i] & 0xF);
            }

            bytesWritten = source.Length * 2;
            return true;
        }

        private static byte GetUpperHexByte(int value) => (byte)(value < 10 ? value + '0' : value - 10 + 'A');
    }
}
