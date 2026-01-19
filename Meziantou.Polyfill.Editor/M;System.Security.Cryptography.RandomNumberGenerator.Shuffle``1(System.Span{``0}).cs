// when M:System.Security.Cryptography.RandomNumberGenerator.GetInt32(System.Int32,System.Int32)
partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static void Shuffle<T>(System.Span<T> values)
        {
            int n = values.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int j = GetInt32(i, n);

                if (i != j)
                {
                    T temp = values[i];
                    values[i] = values[j];
                    values[j] = temp;
                }
            }
        }
    }
}
