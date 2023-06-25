using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// Represents a 7-tuple, or sentuple, as a value type.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
    /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
    /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
    /// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
    /// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
    [StructLayout(LayoutKind.Auto)]
    internal struct ValueTuple<T1, T2, T3, T4, T5, T6, T7>
        : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>, ITupleInternal
    {
        private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;
        private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;
        private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;
        private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;
        private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;
        private static readonly EqualityComparer<T6> s_t6Comparer = EqualityComparer<T6>.Default;
        private static readonly EqualityComparer<T7> s_t7Comparer = EqualityComparer<T7>.Default;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's first component.
        /// </summary>
        public T1 Item1;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's second component.
        /// </summary>
        public T2 Item2;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's third component.
        /// </summary>
        public T3 Item3;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's fourth component.
        /// </summary>
        public T4 Item4;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's fifth component.
        /// </summary>
        public T5 Item5;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's sixth component.
        /// </summary>
        public T6 Item6;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's seventh component.
        /// </summary>
        public T7 Item7;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        /// <param name="item4">The value of the tuple's fourth component.</param>
        /// <param name="item5">The value of the tuple's fifth component.</param>
        /// <param name="item6">The value of the tuple's sixth component.</param>
        /// <param name="item7">The value of the tuple's seventh component.</param>
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object? obj)
        {
            return obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7> && Equals((ValueTuple<T1, T2, T3, T4, T5, T6, T7>)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/>
        /// instance is equal to a specified <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its fields
        /// are equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1, T2, T3, T4, T5, T6, T7> other)
        {
            return s_t1Comparer.Equals(Item1, other.Item1)
                && s_t2Comparer.Equals(Item2, other.Item2)
                && s_t3Comparer.Equals(Item3, other.Item3)
                && s_t4Comparer.Equals(Item4, other.Item4)
                && s_t5Comparer.Equals(Item5, other.Item5)
                && s_t6Comparer.Equals(Item6, other.Item6)
                && s_t7Comparer.Equals(Item7, other.Item7);
        }

        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer)
        {
            if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7>)) return false;

            var objTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7>)other;

            return comparer.Equals(Item1, objTuple.Item1)
                && comparer.Equals(Item2, objTuple.Item2)
                && comparer.Equals(Item3, objTuple.Item3)
                && comparer.Equals(Item4, objTuple.Item4)
                && comparer.Equals(Item5, objTuple.Item5)
                && comparer.Equals(Item6, objTuple.Item6)
                && comparer.Equals(Item7, objTuple.Item7);
        }

        int IComparable.CompareTo(object other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            return CompareTo((ValueTuple<T1, T2, T3, T4, T5, T6, T7>)other);
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1, T2, T3, T4, T5, T6, T7> other)
        {
            int c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            if (c != 0) return c;

            c = Comparer<T2>.Default.Compare(Item2, other.Item2);
            if (c != 0) return c;

            c = Comparer<T3>.Default.Compare(Item3, other.Item3);
            if (c != 0) return c;

            c = Comparer<T4>.Default.Compare(Item4, other.Item4);
            if (c != 0) return c;

            c = Comparer<T5>.Default.Compare(Item5, other.Item5);
            if (c != 0) return c;

            c = Comparer<T6>.Default.Compare(Item6, other.Item6);
            if (c != 0) return c;

            return Comparer<T7>.Default.Compare(Item7, other.Item7);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7>)other;

            int c = comparer.Compare(Item1, objTuple.Item1);
            if (c != 0) return c;

            c = comparer.Compare(Item2, objTuple.Item2);
            if (c != 0) return c;

            c = comparer.Compare(Item3, objTuple.Item3);
            if (c != 0) return c;

            c = comparer.Compare(Item4, objTuple.Item4);
            if (c != 0) return c;

            c = comparer.Compare(Item5, objTuple.Item5);
            if (c != 0) return c;

            c = comparer.Compare(Item6, objTuple.Item6);
            if (c != 0) return c;

            return comparer.Compare(Item7, objTuple.Item7);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Item1, Item2, Item3, Item4, Item5, Item6, Item7);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        private int GetHashCodeCore(IEqualityComparer comparer)
        {
            return HashCode.Combine(comparer.GetHashCode(Item1!),
                                    comparer.GetHashCode(Item2!),
                                    comparer.GetHashCode(Item3!),
                                    comparer.GetHashCode(Item4!),
                                    comparer.GetHashCode(Item5!),
                                    comparer.GetHashCode(Item6!),
                                    comparer.GetHashCode(Item7!));
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1, Item2, Item3, Item4, Item5, Item6, Item7)</c>.
        /// If any field value is <see langword="null"/>, it is represented as <see cref="String.Empty"/>.
        /// </remarks>
        public override string ToString()
        {
            return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " + Item7?.ToString() + ")";
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }
        string ITupleInternal.ToStringEnd()
        {
            return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " + Item7?.ToString() + ")";
        }

        int ITupleInternal.Size => 7;
    }
}