using System;

static partial class PolyfillExtensions
{
    extension(BitConverter)
    {
        public static uint ToUInt32(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToUInt32(value.ToArray(), 0);
        }
    }
}
