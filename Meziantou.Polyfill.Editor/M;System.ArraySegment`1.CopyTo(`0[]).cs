partial class PolyfillExtensions
{
    extension<T>(System.ArraySegment<T> segment)
    {
        public void CopyTo(T[] destination)
        {
            var array = segment.Array;
            if (array is null)
            {
                throw new System.InvalidOperationException("The underlying array is null.");
            }

            System.Array.Copy(array, segment.Offset, destination, 0, segment.Count);
        }
    }
}
