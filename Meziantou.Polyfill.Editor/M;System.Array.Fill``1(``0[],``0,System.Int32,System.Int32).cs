partial class PolyfillExtensions
{
    extension(System.Array)
    {
        public static void Fill<T>(T[] array, T value, int startIndex, int count)
        {
            if (array is null)
            {
                throw new System.ArgumentNullException(nameof(array));
            }

            if ((uint)startIndex > (uint)array.Length)
            {
                throw new System.ArgumentOutOfRangeException(nameof(startIndex));
            }

            if ((uint)count > (uint)(array.Length - startIndex))
            {
                throw new System.ArgumentOutOfRangeException(nameof(count));
            }

            int end = startIndex + count;
            for (int i = startIndex; i < end; i++)
            {
                array[i] = value;
            }
        }
    }
}
