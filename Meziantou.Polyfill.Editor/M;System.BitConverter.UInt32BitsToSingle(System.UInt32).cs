using System;
static partial class PolyfillExtensions_BitConverter
{
    extension(BitConverter)
    {
        public static float UInt32BitsToSingle(uint value) => BitConverter.ToSingle(BitConverter.GetBytes(value), 0);
    }
}
