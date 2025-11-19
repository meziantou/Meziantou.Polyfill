using System;

static partial class PolyfillExtensions
{
    extension(BitConverter)
    {
        public static int ToInt32(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToInt32(value.ToArray(), 0);
        }
    }
}
