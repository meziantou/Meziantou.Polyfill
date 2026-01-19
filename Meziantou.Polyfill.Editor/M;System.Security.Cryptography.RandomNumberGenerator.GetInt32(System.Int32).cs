// when M:System.Security.Cryptography.RandomNumberGenerator.GetInt32(System.Int32,System.Int32)
partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static int GetInt32(int toExclusive)
        {
            System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toExclusive);

            return GetInt32(0, toExclusive);
        }
    }
}
