using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static byte[] FromHexString(string s)
        {
            if (s is null)
                throw new ArgumentNullException(nameof(s));
            if ((s.Length & 1) != 0)
                throw new FormatException();

            var result = new byte[s.Length / 2];
            for (var i = 0; i < result.Length; i++)
            {
                var high = GetHexValueFromString(s[i * 2]);
                var low = GetHexValueFromString(s[(i * 2) + 1]);
                if ((high | low) < 0)
                    throw new FormatException();

                result[i] = (byte)((high << 4) | low);
            }

            return result;
        }

        private static int GetHexValueFromString(char value)
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
