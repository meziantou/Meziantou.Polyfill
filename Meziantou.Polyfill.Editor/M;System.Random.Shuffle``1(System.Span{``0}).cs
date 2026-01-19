using System;

partial class PolyfillExtensions
{
    public static void Shuffle<T>(this Random random, Span<T> values)
    {
        ArgumentNullException.ThrowIfNull(random);

        int n = values.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int j = random.Next(i, n);
            if (j != i)
            {
                T temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }
        }
    }
}
