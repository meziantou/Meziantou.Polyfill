using System;

static partial class PolyfillExtensions
{
    extension(BitConverter)
    {
        public static short ToInt16(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToInt16(value.ToArray(), 0);
        }
    }
}
