using System;
using System.Runtime.InteropServices;

static partial class PolyfillExtensions_MemoryMarshal
{
    extension(MemoryMarshal)
    {
        public static void Write<T>(Span<byte> destination, in T value)
            where T : struct
        {
            // Older frameworks and System.Memory only provide Write<T>(Span<byte>, ref T)
            var copy = value;
            MemoryMarshal.Write(destination, ref copy);
        }
    }
}
