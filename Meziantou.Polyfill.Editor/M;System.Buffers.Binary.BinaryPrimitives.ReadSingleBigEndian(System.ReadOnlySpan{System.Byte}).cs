using System;
using System.Buffers.Binary;

static partial class PolyfillExtensions
{
    extension(BinaryPrimitives)
    {
        public static float ReadSingleBigEndian(ReadOnlySpan<byte> source)
        {
            var bytes = source.Slice(0, sizeof(float)).ToArray();
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToSingle(bytes, 0);
        }
    }
}
