using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static string ToHexString(byte[] inArray)
        {
            if (inArray is null)
                throw new ArgumentNullException(nameof(inArray));

            return ToHexString(new ReadOnlySpan<byte>(inArray));
        }
    }
}
