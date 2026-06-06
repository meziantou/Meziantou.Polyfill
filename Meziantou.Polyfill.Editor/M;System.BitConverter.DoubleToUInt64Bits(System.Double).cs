using System;
static partial class PolyfillExtensions_BitConverter
{
    extension(BitConverter)
    {
        public static ulong DoubleToUInt64Bits(double value) => unchecked((ulong)BitConverter.DoubleToInt64Bits(value));
    }
}
