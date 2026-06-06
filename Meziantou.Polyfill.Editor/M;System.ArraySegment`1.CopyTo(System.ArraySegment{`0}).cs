partial class PolyfillExtensions
{
    extension<T>(System.ArraySegment<T> segment)
    {
        public void CopyTo(System.ArraySegment<T> destination)
        {
            var array = segment.Array;
            if (array is null)
            {
                throw new System.InvalidOperationException("The underlying array is null.");
            }

            var destinationArray = destination.Array;
            if (destinationArray is null)
            {
                throw new System.InvalidOperationException("The underlying array is null.");
            }

            if (segment.Count > destination.Count)
            {
                throw new System.ArgumentException("Destination is too short.");
            }

            System.Array.Copy(array, segment.Offset, destinationArray, destination.Offset, segment.Count);
        }
    }
}
