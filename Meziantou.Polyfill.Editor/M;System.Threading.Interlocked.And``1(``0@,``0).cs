using System;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions_Interlocked
{
    extension(Interlocked)
    {
        public static T And<T>(ref T location1, T value)
            where T : struct
        {
            ValidateAndType<T>();

            if (Unsafe.SizeOf<T>() < 4)
            {
                lock (typeof(PolyfillExtensions_Interlocked))
                {
                    var original = location1;
                    if (Unsafe.SizeOf<T>() == 1)
                    {
                        var result = (byte)(Unsafe.As<T, byte>(ref location1) & Unsafe.As<T, byte>(ref value));
                        location1 = Unsafe.As<byte, T>(ref result);
                    }
                    else
                    {
                        var result = (ushort)(Unsafe.As<T, ushort>(ref location1) & Unsafe.As<T, ushort>(ref value));
                        location1 = Unsafe.As<ushort, T>(ref result);
                    }

                    return original;
                }
            }

            // Interlocked.And is unavailable on some target frameworks and would resolve back to this polyfill.
            if (Unsafe.SizeOf<T>() == 4)
            {
                ref var target = ref Unsafe.As<T, int>(ref location1);
                var operand = Unsafe.As<T, int>(ref value);
                var current = target;
                while (true)
                {
                    var original = Interlocked.CompareExchange(ref target, current & operand, current);
                    if (original == current)
                        return Unsafe.As<int, T>(ref original);

                    current = original;
                }
            }

            ref var target64 = ref Unsafe.As<T, long>(ref location1);
            var operand64 = Unsafe.As<T, long>(ref value);
            var current64 = target64;
            while (true)
            {
                var original = Interlocked.CompareExchange(ref target64, current64 & operand64, current64);
                if (original == current64)
                    return Unsafe.As<long, T>(ref original);

                current64 = original;
            }
        }

        private static void ValidateAndType<T>()
            where T : struct
        {
            if ((!typeof(T).IsPrimitive && !typeof(T).IsEnum) || typeof(T) == typeof(float) || typeof(T) == typeof(double))
                throw new NotSupportedException("The type must be an integer primitive type or an enum type backed by an integer type.");
        }

    }
}
