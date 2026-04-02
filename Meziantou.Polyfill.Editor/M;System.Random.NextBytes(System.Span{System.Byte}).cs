using System;

partial class PolyfillExtensions
{
    public static void NextBytes(this Random random, Span<byte> buffer)
    {
        if (random is null)
        {
            throw new ArgumentNullException(nameof(random));
        }

        byte[] array = new byte[buffer.Length];
        random.NextBytes(array);
        array.AsSpan().CopyTo(buffer);
    }
}
