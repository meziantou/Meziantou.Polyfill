using System;
using System.Buffers.Binary;

static partial class PolyfillExtensions
{
    extension(BinaryPrimitives)
    {
        public static bool TryWriteDoubleLittleEndian(Span<byte> destination, double value)
        {
            if (destination.Length < sizeof(double))
                return false;

            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            bytes.AsSpan().CopyTo(destination);
            return true;
        }
    }
}
