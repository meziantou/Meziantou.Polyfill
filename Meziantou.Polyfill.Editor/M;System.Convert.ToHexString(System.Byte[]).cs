using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static string ToHexString(byte[] inArray)
        {
            if (inArray == null)
                throw new ArgumentNullException(nameof(inArray));

            return ToHexString(inArray, 0, inArray.Length);
        }
    }
}
