using System;
using System.Buffers;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static OperationStatus FromHexString(string source, Span<byte> destination, out int charsConsumed, out int bytesWritten)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            var pairsToDecode = Math.Min(source.Length / 2, destination.Length);
            for (var i = 0; i < pairsToDecode; i++)
            {
                var high = GetHexValueFromStringStatus(source[i * 2]);
                var low = GetHexValueFromStringStatus(source[(i * 2) + 1]);
                if ((high | low) < 0)
                {
                    charsConsumed = i * 2;
                    bytesWritten = i;
                    return OperationStatus.InvalidData;
                }

                destination[i] = (byte)((high << 4) | low);
            }

            charsConsumed = pairsToDecode * 2;
            bytesWritten = pairsToDecode;
            if (destination.Length < source.Length / 2)
                return OperationStatus.DestinationTooSmall;

            return (source.Length & 1) != 0 ? OperationStatus.NeedMoreData : OperationStatus.Done;
        }

        private static int GetHexValueFromStringStatus(char value)
        {
            if (value >= '0' && value <= '9')
                return value - '0';
            if (value >= 'A' && value <= 'F')
                return value - 'A' + 10;
            if (value >= 'a' && value <= 'f')
                return value - 'a' + 10;
            return -1;
        }
    }
}
