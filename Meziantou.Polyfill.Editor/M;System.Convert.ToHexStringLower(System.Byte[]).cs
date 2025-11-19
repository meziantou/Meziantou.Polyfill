using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static string ToHexStringLower(byte[] inArray)
        {
            if (inArray == null)
                throw new ArgumentNullException(nameof(inArray));

            return ToHexStringLower(inArray, 0, inArray.Length);
        }
    }
}
