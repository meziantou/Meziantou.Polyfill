using System;
using System.Runtime.CompilerServices;

static partial class PolyfillExtensions_Nullable
{
    extension(Nullable)
    {
        public static ref readonly T GetValueRefOrDefaultRef<T>(ref readonly T? nullable)
            where T : struct
        {
            ref var nullableData = ref Unsafe.As<T?, NullableValueStorage<T>>(ref Unsafe.AsRef(in nullable));
            return ref nullableData.Value;
        }
    }
}

file struct NullableValueStorage<T>
    where T : struct
{
    public bool HasValue;
    public T Value;
}
