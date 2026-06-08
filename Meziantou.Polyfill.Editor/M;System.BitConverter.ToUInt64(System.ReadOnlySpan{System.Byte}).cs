using System;

static partial class PolyfillExtensions
{
    extension(BitConverter)
    {
        public static ulong ToUInt64(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToUInt64(value.ToArray(), 0);
        }
    }
}