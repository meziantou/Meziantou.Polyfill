using System;
using System.Buffers.Binary;

static partial class PolyfillExtensions
{
    extension(BinaryPrimitives)
    {
        public static void WriteDoubleLittleEndian(Span<byte> destination, double value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            bytes.AsSpan().CopyTo(destination.Slice(0, sizeof(double)));
        }
    }
}
