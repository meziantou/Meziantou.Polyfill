using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static byte[] FromHexString(ReadOnlySpan<byte> utf8Source)
        {
            if ((utf8Source.Length & 1) != 0)
                throw new FormatException();

            var result = new byte[utf8Source.Length / 2];
            for (var i = 0; i < result.Length; i++)
            {
                var high = GetHexValueFromUtf8(utf8Source[i * 2]);
                var low = GetHexValueFromUtf8(utf8Source[(i * 2) + 1]);
                if ((high | low) < 0)
                    throw new FormatException();

                result[i] = (byte)((high << 4) | low);
            }

            return result;
        }

        private static int GetHexValueFromUtf8(byte value)
        {
            if (value >= (byte)'0' && value <= (byte)'9')
                return value - '0';
            if (value >= (byte)'A' && value <= (byte)'F')
                return value - 'A' + 10;
            if (value >= (byte)'a' && value <= (byte)'f')
                return value - 'a' + 10;
            return -1;
        }
    }
}
