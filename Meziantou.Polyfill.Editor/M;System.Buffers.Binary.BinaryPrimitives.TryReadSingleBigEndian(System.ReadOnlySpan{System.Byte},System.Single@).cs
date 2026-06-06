using System;
using System.Buffers.Binary;

static partial class PolyfillExtensions
{
    extension(BinaryPrimitives)
    {
        public static bool TryReadSingleBigEndian(ReadOnlySpan<byte> source, out float value)
        {
            if (source.Length < sizeof(float))
            {
                value = default;
                return false;
            }

            var bytes = source.Slice(0, sizeof(float)).ToArray();
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            value = BitConverter.ToSingle(bytes, 0);
            return true;
        }
    }
}
