using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static bool TryToHexString(ReadOnlySpan<byte> source, Span<char> destination, out int charsWritten)
        {
            if (source.Length > int.MaxValue / 2 || destination.Length < source.Length * 2)
            {
                charsWritten = 0;
                return false;
            }

            for (var i = 0; i < source.Length; i++)
            {
                destination[i * 2] = GetUpperHexChar(source[i] >> 4);
                destination[(i * 2) + 1] = GetUpperHexChar(source[i] & 0xF);
            }

            charsWritten = source.Length * 2;
            return true;
        }

        private static char GetUpperHexChar(int value) => (char)(value < 10 ? value + '0' : value - 10 + 'A');
    }
}
