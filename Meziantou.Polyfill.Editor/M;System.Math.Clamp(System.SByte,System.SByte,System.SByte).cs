#nullable enable

static partial class PolyfillExtensions_SByte
{
    extension(System.Math)
    {
        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>value if min ≤ value ≤ max. -or- min if value &lt; min. -or- max if max &lt; value.</returns>
        /// <exception cref="System.ArgumentException">max is less than min.</exception>
        [System.CLSCompliant(false)]
        public static sbyte Clamp(sbyte value, sbyte min, sbyte max)
        {
            if (min > max)
                ThrowHelper.ThrowMinMaxException(min, max);

            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }
    }
}

file static class ThrowHelper
{
    public static void ThrowMinMaxException<T>(T min, T max) => throw new System.ArgumentException($"'{nameof(min)}' cannot be greater than {nameof(max)}.");
}
