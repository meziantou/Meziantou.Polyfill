using System;

static partial class PolyfillExtensions
{
    extension(BitConverter)
    {
        public static ushort ToUInt16(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToUInt16(value.ToArray(), 0);
        }
    }
}
