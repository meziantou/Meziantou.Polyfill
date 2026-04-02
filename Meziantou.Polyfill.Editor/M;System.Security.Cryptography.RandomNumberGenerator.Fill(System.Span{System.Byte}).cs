partial class PolyfillExtensions
{
    public static void Fill(this System.Security.Cryptography.RandomNumberGenerator random, System.Span<byte> data)
    {
        if (random is null)
        {
            throw new System.ArgumentNullException(nameof(random));
        }

        if (data.IsEmpty)
            return;

        byte[] array = new byte[data.Length];
        try
        {
            random.GetBytes(array);
            new System.ReadOnlySpan<byte>(array).CopyTo(data);
        }
        finally
        {
            System.Array.Clear(array, 0, array.Length);
        }
    }
}
