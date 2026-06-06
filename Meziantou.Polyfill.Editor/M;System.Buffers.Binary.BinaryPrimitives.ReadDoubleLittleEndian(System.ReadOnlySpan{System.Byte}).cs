using System;
using System.Buffers.Binary;

static partial class PolyfillExtensions
{
    extension(BinaryPrimitives)
    {
        public static double ReadDoubleLittleEndian(ReadOnlySpan<byte> source)
        {
            var bytes = source.Slice(0, sizeof(double)).ToArray();
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToDouble(bytes, 0);
        }
    }
}
