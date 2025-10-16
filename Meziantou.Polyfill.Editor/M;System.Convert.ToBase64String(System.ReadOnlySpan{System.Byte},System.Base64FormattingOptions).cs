using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static string ToBase64String(ReadOnlySpan<byte> bytes, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return Convert.ToBase64String(bytes.ToArray(), options);
        }
    }
}