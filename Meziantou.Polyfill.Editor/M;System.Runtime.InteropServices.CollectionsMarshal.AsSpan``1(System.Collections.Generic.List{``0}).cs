using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
    internal static partial class CollectionsMarshal
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> AsSpan<T>(List<T>? list)
        {
            Span<T> span = default;
            if (list is not null)
            {
                T[] items = ListFields<T>.GetItems(list);
                int size = list.Count;

                if ((uint)size > (uint)items.Length)
                {
                    throw new InvalidOperationException("Concurrent operations are not supported.");
                }

                span = new Span<T>(items, 0, size);
            }
            return span;
        }

        private static class ListFields<T>
        {
            private static readonly FieldInfo ItemsField =
                typeof(List<T>).GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic)
                ?? throw new MissingFieldException(typeof(List<T>).FullName, "_items");

            public static T[] GetItems(List<T> list)
            {
                return (T[])ItemsField.GetValue(list)!;
            }
        }
    }
}
