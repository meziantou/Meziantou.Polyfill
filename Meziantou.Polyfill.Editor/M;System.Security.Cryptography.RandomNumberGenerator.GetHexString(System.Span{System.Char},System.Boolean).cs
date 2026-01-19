partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static void GetHexString(System.Span<char> destination, bool lowercase = false)
        {
            if (destination.IsEmpty)
                return;

            int byteCount = (destination.Length + 1) / 2;
            byte[] bytes = new byte[byteCount];

            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            string hexChars = lowercase ? "0123456789abcdef" : "0123456789ABCDEF";

            for (int i = 0; i < destination.Length; i++)
            {
                int byteIndex = i / 2;
                int nibble = (i % 2 == 0) ? (bytes[byteIndex] >> 4) : (bytes[byteIndex] & 0xF);
                destination[i] = hexChars[nibble];
            }
        }
    }
}
