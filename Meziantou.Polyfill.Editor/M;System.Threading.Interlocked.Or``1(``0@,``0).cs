using System;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions_Interlocked
{
    extension(Interlocked)
    {
        public static unsafe T Or<T>(ref T location1, T value)
            where T : struct
        {
            ValidateOrType<T>();

            if (Unsafe.SizeOf<T>() == 1)
                return ApplyOrByte(ref location1, value);
            if (Unsafe.SizeOf<T>() == 2)
                return ApplyOrUInt16(ref location1, value);
            if (Unsafe.SizeOf<T>() == 4)
            {
                var original = Interlocked.Or(ref Unsafe.As<T, int>(ref location1), Unsafe.As<T, int>(ref value));
                return Unsafe.As<int, T>(ref original);
            }

            var original64 = Interlocked.Or(ref Unsafe.As<T, long>(ref location1), Unsafe.As<T, long>(ref value));
            return Unsafe.As<long, T>(ref original64);
        }

        private static void ValidateOrType<T>()
            where T : struct
        {
            if ((!typeof(T).IsPrimitive && !typeof(T).IsEnum) || typeof(T) == typeof(float) || typeof(T) == typeof(double))
                throw new NotSupportedException("The type must be an integer primitive type or an enum type backed by an integer type.");
        }

        private static unsafe T ApplyOrByte<T>(ref T location1, T value)
            where T : struct
        {
            ref var byteLocation = ref Unsafe.As<T, byte>(ref location1);
            fixed (byte* pointer = &byteLocation)
            {
                var offset = (int)((nuint)pointer & 3);
                ref var alignedLocation = ref Unsafe.AsRef<int>(pointer - offset);
                var shift = (BitConverter.IsLittleEndian ? offset : 3 - offset) * 8;
                var mask = 0xff << shift;
                var shiftedValue = Unsafe.As<T, byte>(ref value) << shift;
                var current = alignedLocation;
                while (true)
                {
                    var newValue = (current & ~mask) | ((current & mask) | shiftedValue);
                    var original = Interlocked.CompareExchange(ref alignedLocation, newValue, current);
                    if (original == current)
                    {
                        var result = (byte)(original >> shift);
                        return Unsafe.As<byte, T>(ref result);
                    }

                    current = original;
                }
            }
        }

        private static unsafe T ApplyOrUInt16<T>(ref T location1, T value)
            where T : struct
        {
            ref var byteLocation = ref Unsafe.As<T, byte>(ref location1);
            fixed (byte* pointer = &byteLocation)
            {
                var offset = (int)((nuint)pointer & 3);
                ref var alignedLocation = ref Unsafe.AsRef<int>(pointer - offset);
                var shift = (BitConverter.IsLittleEndian ? offset : 2 - offset) * 8;
                var mask = 0xffff << shift;
                var shiftedValue = Unsafe.As<T, ushort>(ref value) << shift;
                var current = alignedLocation;
                while (true)
                {
                    var newValue = (current & ~mask) | ((current & mask) | shiftedValue);
                    var original = Interlocked.CompareExchange(ref alignedLocation, newValue, current);
                    if (original == current)
                    {
                        var result = (ushort)(original >> shift);
                        return Unsafe.As<ushort, T>(ref result);
                    }

                    current = original;
                }
            }
        }
    }
}
