using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static string ToHexStringLower(byte[] inArray, int offset, int length)
        {
            if (inArray == null)
                throw new ArgumentNullException(nameof(inArray));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Non-negative number required.");
            if (offset > inArray.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), "Offset must be within the array.");
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Non-negative number required.");
            if (offset > inArray.Length - length)
                throw new ArgumentOutOfRangeException(nameof(length), "Offset and length must refer to a position in the array.");

            if (length == 0)
                return string.Empty;

            const int AddToAlpha = 87;
            const int AddToDigit = -39;

            var c = new char[length * 2];
            for (var i = 0; i < length; i++)
            {
                var byteValue = inArray[offset + i];
                var b = byteValue >> 4;
                c[i * 2] = (char)(AddToAlpha + b + (((b - 10) >> 31) & AddToDigit));

                b = byteValue & 0xF;
                c[(i * 2) + 1] = (char)(AddToAlpha + b + (((b - 10) >> 31) & AddToDigit));
            }

            return new string(c);
        }
    }
}
