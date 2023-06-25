using System.Collections;
using System.Collections.Generic;

namespace System
{
    internal struct ValueTuple<T1> : IEquatable<ValueTuple<T1>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1>>, ITupleInternal
    {
        private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

        /// <summary>
        /// The current <see cref="ValueTuple{T1}"/> instance's first component.
        /// </summary>
        public T1 Item1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        public ValueTuple(T1 item1)
        {
            Item1 = item1;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object? obj)
        {
            return obj is ValueTuple<T1> && Equals((ValueTuple<T1>)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1}"/>
        /// instance is equal to a specified <see cref="ValueTuple{T1}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its field
        /// is equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1> other)
        {
            return s_t1Comparer.Equals(Item1, other.Item1);
        }

        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer)
        {
            if (other == null || !(other is ValueTuple<T1>)) return false;

            var objTuple = (ValueTuple<T1>)other;

            return comparer.Equals(Item1, objTuple.Item1);
        }

        int IComparable.CompareTo(object? other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1>)other;

            return Comparer<T1>.Default.Compare(Item1, objTuple.Item1);
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1> other)
        {
            return Comparer<T1>.Default.Compare(Item1, other.Item1);
        }

        int IStructuralComparable.CompareTo(object? other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1>)other;

            return comparer.Compare(Item1, objTuple.Item1);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return s_t1Comparer.GetHashCode(Item1);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(Item1);
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1)</c>,
        /// where <c>Item1</c> represents the value of <see cref="Item1"/>. If the field is <see langword="null"/>,
        /// it is represented as <see cref="string.Empty"/>.
        /// </remarks>
        public override string ToString()
        {
            return "(" + Item1?.ToString() + ")";
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(Item1);
        }

        string ITupleInternal.ToStringEnd()
        {
            return Item1?.ToString() + ")";
        }

        int ITupleInternal.Size => 1;
    }
}