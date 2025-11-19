using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static string ToHexString(byte[] inArray, int offset, int length)
        {
            if (inArray is null)
                throw new ArgumentNullException(nameof(inArray));

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            if (offset > inArray.Length - length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return ToHexString(new ReadOnlySpan<byte>(inArray, offset, length));
        }
    }
}
