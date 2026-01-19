partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static string GetHexString(int stringLength, bool lowercase = false)
        {
            if (stringLength < 0)
                throw new System.ArgumentOutOfRangeException(nameof(stringLength), "stringLength must be non-negative.");

            if (stringLength == 0)
                return string.Empty;

            int byteCount = (stringLength + 1) / 2;
            byte[] bytes = new byte[byteCount];

            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            char[] chars = new char[stringLength];
            string hexChars = lowercase ? "0123456789abcdef" : "0123456789ABCDEF";

            for (int i = 0; i < stringLength; i++)
            {
                int byteIndex = i / 2;
                int nibble = (i % 2 == 0) ? (bytes[byteIndex] >> 4) : (bytes[byteIndex] & 0xF);
                chars[i] = hexChars[nibble];
            }

            return new string(chars);
        }
    }
}
