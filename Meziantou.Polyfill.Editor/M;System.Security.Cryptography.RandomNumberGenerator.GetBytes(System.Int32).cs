partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static byte[] GetBytes(int count)
        {
            System.ArgumentOutOfRangeException.ThrowIfNegative(count);

            byte[] ret = new byte[count];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(ret);
            }
            return ret;
        }
    }
}
