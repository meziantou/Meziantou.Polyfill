using System;
using System.Buffers.Binary;

static partial class PolyfillExtensions
{
    extension(BinaryPrimitives)
    {
        public static void WriteSingleLittleEndian(Span<byte> destination, float value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            bytes.AsSpan().CopyTo(destination.Slice(0, sizeof(float)));
        }
    }
}
