partial class PolyfillExtensions
{
    extension(System.Array)
    {
        public static void Fill<T>(T[] array, T value)
        {
            if (array is null)
            {
                throw new System.ArgumentNullException(nameof(array));
            }

            System.Array.Fill(array, value, 0, array.Length);
        }
    }
}
