using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    extension<T>(List<T> list)
    {
        /// <summary>Adds the elements of the specified span to the end of the <see cref="List{T}"/>.</summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to which the elements should be added.</param>
        /// <param name="source">The span whose elements should be added to the end of the <see cref="List{T}"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="list"/> is null.</exception>
        public void AddRange(ReadOnlySpan<T> source)
        {
            ArgumentNullException.ThrowIfNull(list);

            if (!source.IsEmpty)
            {
                if (list.Capacity < list.Count + source.Length)
                {
                    list.Capacity = list.Count + source.Length;
                    foreach (var item in source)
                    {
                        list.Add(item);
                    }
                }
            }
        }
    }
}