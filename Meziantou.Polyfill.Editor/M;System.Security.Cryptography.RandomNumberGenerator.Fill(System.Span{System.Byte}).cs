partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static void Fill(System.Span<byte> data)
        {
            if (data.IsEmpty)
                return;

            byte[] array = new byte[data.Length];
            try
            {
                using (var random = System.Security.Cryptography.RandomNumberGenerator.Create())
                {
                    random.GetBytes(array);
                }

                new System.ReadOnlySpan<byte>(array).CopyTo(data);
            }
            finally
            {
                System.Array.Clear(array, 0, array.Length);
            }
        }
    }
}
