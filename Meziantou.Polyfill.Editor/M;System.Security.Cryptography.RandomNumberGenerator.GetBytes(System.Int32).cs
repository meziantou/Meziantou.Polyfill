partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static byte[] GetBytes(int count)
        {
            if (count < 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(count), count, "must not be negative.");
            }

            byte[] ret = new byte[count];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(ret);
            }
            return ret;
        }
    }
}
