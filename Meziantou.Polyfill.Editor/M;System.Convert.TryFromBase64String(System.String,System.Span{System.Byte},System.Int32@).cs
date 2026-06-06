using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static bool TryFromBase64String(string s, Span<byte> bytes, out int bytesWritten)
        {
            if (s is null)
                throw new ArgumentNullException(nameof(s));

            try
            {
                var result = Convert.FromBase64String(s);
                if (result.Length > bytes.Length)
                {
                    bytesWritten = 0;
                    return false;
                }

                result.AsSpan().CopyTo(bytes);
                bytesWritten = result.Length;
                return true;
            }
            catch (FormatException)
            {
                bytesWritten = 0;
                return false;
            }
        }
    }
}
