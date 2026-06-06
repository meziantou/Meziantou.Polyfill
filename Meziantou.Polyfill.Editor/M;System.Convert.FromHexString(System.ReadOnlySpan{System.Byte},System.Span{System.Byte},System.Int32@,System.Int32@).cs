using System;
using System.Buffers;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static OperationStatus FromHexString(ReadOnlySpan<byte> utf8Source, Span<byte> destination, out int bytesConsumed, out int bytesWritten)
        {
            var pairsToDecode = Math.Min(utf8Source.Length / 2, destination.Length);
            for (var i = 0; i < pairsToDecode; i++)
            {
                var high = GetHexValueFromUtf8Status(utf8Source[i * 2]);
                var low = GetHexValueFromUtf8Status(utf8Source[(i * 2) + 1]);
                if ((high | low) < 0)
                {
                    bytesConsumed = i * 2;
                    bytesWritten = i;
                    return OperationStatus.InvalidData;
                }

                destination[i] = (byte)((high << 4) | low);
            }

            bytesConsumed = pairsToDecode * 2;
            bytesWritten = pairsToDecode;
            if (destination.Length < utf8Source.Length / 2)
                return OperationStatus.DestinationTooSmall;

            return (utf8Source.Length & 1) != 0 ? OperationStatus.NeedMoreData : OperationStatus.Done;
        }

        private static int GetHexValueFromUtf8Status(byte value)
        {
            if (value >= (byte)'0' && value <= (byte)'9')
                return value - '0';
            if (value >= (byte)'A' && value <= (byte)'F')
                return value - 'A' + 10;
            if (value >= (byte)'a' && value <= (byte)'f')
                return value - 'a' + 10;
            return -1;
        }
    }
}
