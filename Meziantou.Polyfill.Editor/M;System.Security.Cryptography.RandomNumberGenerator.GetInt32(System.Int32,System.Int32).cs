partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static int GetInt32(int fromInclusive, int toExclusive)
        {
            if (fromInclusive >= toExclusive)
                throw new System.ArgumentException("Range of random number does not contain at least one possibility.");

            uint range = (uint)toExclusive - (uint)fromInclusive - 1;

            uint mask = range;
            mask |= mask >> 1;
            mask |= mask >> 2;
            mask |= mask >> 4;
            mask |= mask >> 8;
            mask |= mask >> 16;

            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[4];
                uint result;

                do
                {
                    rng.GetBytes(bytes);
                    result = mask & System.BitConverter.ToUInt32(bytes, 0);
                }
                while (result > range);

                return (int)result + fromInclusive;
            }
        }
    }
}
