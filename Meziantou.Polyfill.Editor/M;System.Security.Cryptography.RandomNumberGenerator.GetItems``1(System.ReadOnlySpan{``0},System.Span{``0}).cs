// when M:System.Security.Cryptography.RandomNumberGenerator.GetInt32(System.Int32)
partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static void GetItems<T>(System.ReadOnlySpan<T> choices, System.Span<T> destination)
        {
            if (choices.IsEmpty)
                throw new System.ArgumentException("choices cannot be empty.", nameof(choices));

            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                for (int i = 0; i < destination.Length; i++)
                {
                    destination[i] = choices[GetInt32(choices.Length)];
                }
            }
        }
    }
}
