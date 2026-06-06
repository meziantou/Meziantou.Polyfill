// define-type System.Numerics.IAdditionOperators`3
using System;
using System.Collections.Generic;
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NUMERICS_IADDITIONOPERATORS_3
using System.Numerics;
#endif

static partial class PolyfillExtensions
{
    extension(System.Linq.Enumerable)
    {
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NUMERICS_IADDITIONOPERATORS_3
        public static IEnumerable<T> InfiniteSequence<T>(T start, T step) where T : IAdditionOperators<T, T, T>
#else
        public static IEnumerable<T> InfiniteSequence<T>(T start, T step)
#endif
        {
            while (true)
            {
                yield return start;
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NUMERICS_IADDITIONOPERATORS_3
                start += step;
#else
                start = InfiniteSequenceNumericOperations<T>.Add(start, step);
#endif
            }
        }
    }
}

file static class InfiniteSequenceNumericOperations<T>
{
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
