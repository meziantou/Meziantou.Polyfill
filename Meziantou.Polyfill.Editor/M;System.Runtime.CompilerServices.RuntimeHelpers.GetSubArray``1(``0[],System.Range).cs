// Note: this polyfill only supports explicit calls to RuntimeHelpers.GetSubArray.
// The compiler requires a real static member on RuntimeHelpers to lower array range syntax (array[a..b]),
// so extension members cannot enable it.
static partial class PolyfillExtensions
{
    extension(global::System.Runtime.CompilerServices.RuntimeHelpers)
    {
        public static T[] GetSubArray<T>(T[] array, global::System.Range range)
        {
            if (array is null)
                throw new global::System.ArgumentNullException(nameof(array));

            var startIndex = range.Start;
            var offset = startIndex.IsFromEnd ? array.Length - startIndex.Value : startIndex.Value;
            var endIndex = range.End;
            var end = endIndex.IsFromEnd ? array.Length - endIndex.Value : endIndex.Value;

            if ((uint)end > (uint)array.Length || (uint)offset > (uint)end)
                throw new global::System.ArgumentOutOfRangeException(nameof(range));

            var length = end - offset;

            if (typeof(T[]) == array.GetType())
            {
                if (length == 0)
                    return global::System.Array.Empty<T>();

                var destination = new T[length];
                global::System.Array.Copy(array, offset, destination, 0, length);
                return destination;
            }

            var result = (T[])global::System.Array.CreateInstance(array.GetType().GetElementType()!, length);
            global::System.Array.Copy(array, offset, result, 0, length);
            return result;
        }
    }
}
