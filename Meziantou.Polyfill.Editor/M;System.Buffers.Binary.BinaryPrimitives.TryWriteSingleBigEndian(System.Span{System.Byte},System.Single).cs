using System;
using System.Buffers.Binary;

static partial class PolyfillExtensions
{
    extension(BinaryPrimitives)
    {
        public static bool TryWriteSingleBigEndian(Span<byte> destination, float value)
        {
            if (destination.Length < sizeof(float))
                return false;

            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            bytes.AsSpan().CopyTo(destination);
            return true;
        }
    }
}
