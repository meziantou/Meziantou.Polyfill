using System;
using System.Buffers.Binary;

static partial class PolyfillExtensions
{
    extension(BinaryPrimitives)
    {
        public static bool TryReadDoubleBigEndian(ReadOnlySpan<byte> source, out double value)
        {
            if (source.Length < sizeof(double))
            {
                value = default;
                return false;
            }

            var bytes = source.Slice(0, sizeof(double)).ToArray();
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            value = BitConverter.ToDouble(bytes, 0);
            return true;
        }
    }
}
