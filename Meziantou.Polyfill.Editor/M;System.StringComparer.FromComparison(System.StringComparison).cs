using System;

static partial class PolyfillExtensions
{
    extension(StringComparer)
    {
        /// <summary>
        /// Converts the specified <see cref="StringComparison"/> to a <see cref="StringComparer"/> instance.
        /// </summary>
        /// <param name="comparisonType">
        /// A string comparer instance to convert.
        /// </param>
        /// <returns>
        /// A <see cref="StringComparer"/> instance representing the equivalent value of the specified <see cref="StringComparison"/> instance.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the specified <see cref="StringComparison"/> value is not supported.
        /// </exception>
        public static StringComparer FromComparison(StringComparison comparisonType)
        {
            return comparisonType switch
            {
                StringComparison.CurrentCulture => StringComparer.CurrentCulture,
                StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
                StringComparison.InvariantCulture => StringComparer.InvariantCulture,
                StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
                StringComparison.Ordinal => StringComparer.Ordinal,
                StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
                _ => throw new ArgumentException("The string comparison type passed in is currently not supported.", nameof(comparisonType)),
            };
        }
    }
}