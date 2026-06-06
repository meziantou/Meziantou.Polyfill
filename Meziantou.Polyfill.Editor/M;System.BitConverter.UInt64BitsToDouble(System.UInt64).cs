using System;
static partial class PolyfillExtensions_BitConverter
{
    extension(BitConverter)
    {
        public static double UInt64BitsToDouble(ulong value) => BitConverter.Int64BitsToDouble(unchecked((long)value));
    }
}
