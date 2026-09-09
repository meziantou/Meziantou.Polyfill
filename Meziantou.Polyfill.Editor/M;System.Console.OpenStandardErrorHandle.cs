using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

static partial class PolyfillExtensions
{
    extension(Console)
    {
        public static SafeFileHandle OpenStandardErrorHandle() => new(ConsoleOpenStandardErrorHandle.GetHandle(), ownsHandle: false);
    }
}

file static class ConsoleOpenStandardErrorHandle
{
    public static IntPtr GetHandle() => Environment.OSVersion.Platform == PlatformID.Win32NT ? GetStdHandle(-12) : new IntPtr(2);

#if MEZIANTOU_POLYFILL_SUPPORT_UPDATED_MEMORY_SAFETY_RULES
    [DllImport("kernel32.dll")]
    private static safe extern IntPtr GetStdHandle(int handleType);
#else
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetStdHandle(int handleType);
#endif
}
