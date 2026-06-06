// define-type System.Numerics.INumber`1
using System;
using System.Collections.Generic;
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NUMERICS_INUMBER_1
using System.Numerics;
#endif

static partial class PolyfillExtensions
{
    extension(System.Linq.Enumerable)
    {
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NUMERICS_INUMBER_1
        public static IEnumerable<T> Sequence<T>(T start, T endInclusive, T step) where T : INumber<T>
#else
        public static IEnumerable<T> Sequence<T>(T start, T endInclusive, T step)
#endif
        {
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NUMERICS_INUMBER_1
            if (T.IsNaN(start))
                throw new ArgumentOutOfRangeException(nameof(start));
            if (T.IsNaN(endInclusive))
                throw new ArgumentOutOfRangeException(nameof(endInclusive));
            if (T.IsNaN(step))
                throw new ArgumentOutOfRangeException(nameof(step));
            if (step > T.Zero && endInclusive < start)
                throw new ArgumentOutOfRangeException(nameof(step));
            if (step < T.Zero && endInclusive > start)
                throw new ArgumentOutOfRangeException(nameof(step));
            if (step == T.Zero && endInclusive != start)
                throw new ArgumentOutOfRangeException(nameof(step));
            if (step == T.Zero)
            {
                yield return start;
                yield break;
            }

            if (step > T.Zero)
            {
                var current = start;
                while (current <= endInclusive)
                {
                    yield return current;

                    var next = current + step;
                    if (next <= current)
                        yield break;

                    current = next;
                }
            }
            else
            {
                var current = start;
                while (current >= endInclusive)
                {
                    yield return current;

                    var next = current + step;
                    if (next >= current)
                        yield break;

                    current = next;
                }
            }
#else
            SequenceNumericOperations<T>.EnsureSupported();
            if (SequenceNumericOperations<T>.IsNaN(start))
                throw new ArgumentOutOfRangeException(nameof(start));
            if (SequenceNumericOperations<T>.IsNaN(endInclusive))
                throw new ArgumentOutOfRangeException(nameof(endInclusive));
            if (SequenceNumericOperations<T>.IsNaN(step))
                throw new ArgumentOutOfRangeException(nameof(step));

            var direction = SequenceNumericOperations<T>.Compare(step, default!);
            if (direction > 0 && SequenceNumericOperations<T>.Compare(endInclusive, start) < 0)
                throw new ArgumentOutOfRangeException(nameof(step));
            if (direction < 0 && SequenceNumericOperations<T>.Compare(endInclusive, start) > 0)
                throw new ArgumentOutOfRangeException(nameof(step));
            if (direction == 0 && SequenceNumericOperations<T>.Compare(endInclusive, start) != 0)
                throw new ArgumentOutOfRangeException(nameof(step));
            if (direction == 0)
            {
                yield return start;
                yield break;
            }

            var current = start;
            while (direction > 0
                ? SequenceNumericOperations<T>.Compare(current, endInclusive) <= 0
                : SequenceNumericOperations<T>.Compare(current, endInclusive) >= 0)
            {
                yield return current;

                var next = SequenceNumericOperations<T>.Add(current, step);
                if (direction > 0
                    ? SequenceNumericOperations<T>.Compare(next, current) <= 0
                    : SequenceNumericOperations<T>.Compare(next, current) >= 0)
                {
                    yield break;
                }

                current = next;
            }
#endif
        }
    }
}

file static class SequenceNumericOperations<T>
{
    public static void EnsureSupported()
    {
        if (typeof(T) != typeof(sbyte) && typeof(T) != typeof(byte) &&
            typeof(T) != typeof(short) && typeof(T) != typeof(ushort) &&
            typeof(T) != typeof(int) && typeof(T) != typeof(uint) &&
            typeof(T) != typeof(long) && typeof(T) != typeof(ulong) &&
            typeof(T) != typeof(float) && typeof(T) != typeof(double) &&
            typeof(T) != typeof(decimal))
        {
            throw new InvalidOperationException($"Type '{typeof(T).AssemblyQualifiedName}' is not supported.");
        }
    }

    public static bool IsNaN(T value)
    {
        if (typeof(T) == typeof(float))
            return float.IsNaN((float)(object)value!);
        if (typeof(T) == typeof(double))
            return double.IsNaN((double)(object)value!);

        return false;
    }

    public static int Compare(T left, T right) => Comparer<T>.Default.Compare(left, right);

    public static T Add(T left, T right)
    {
        if (typeof(T) == typeof(sbyte))
            return (T)(object)unchecked((sbyte)((sbyte)(object)left! + (sbyte)(object)right!));
        if (typeof(T) == typeof(byte))
            return (T)(object)unchecked((byte)((byte)(object)left! + (byte)(object)right!));
        if (typeof(T) == typeof(short))
            return (T)(object)unchecked((short)((short)(object)left! + (short)(object)right!));
        if (typeof(T) == typeof(ushort))
            return (T)(object)unchecked((ushort)((ushort)(object)left! + (ushort)(object)right!));
        if (typeof(T) == typeof(int))
            return (T)(object)unchecked((int)(object)left! + (int)(object)right!);
        if (typeof(T) == typeof(uint))
            return (T)(object)unchecked((uint)(object)left! + (uint)(object)right!);
        if (typeof(T) == typeof(long))
            return (T)(object)unchecked((long)(object)left! + (long)(object)right!);
        if (typeof(T) == typeof(ulong))
            return (T)(object)unchecked((ulong)(object)left! + (ulong)(object)right!);
        if (typeof(T) == typeof(float))
            return (T)(object)((float)(object)left! + (float)(object)right!);
        if (typeof(T) == typeof(double))
            return (T)(object)((double)(object)left! + (double)(object)right!);
        if (typeof(T) == typeof(decimal))
            return (T)(object)((decimal)(object)left! + (decimal)(object)right!);

        throw new InvalidOperationException($"Type '{typeof(T).AssemblyQualifiedName}' is not supported.");
    }
}
