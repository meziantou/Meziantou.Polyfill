using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

static partial class PolyfillExtensions
{
    extension(Console)
    {
        public static SafeFileHandle OpenStandardOutputHandle() => new(ConsoleOpenStandardOutputHandle.GetHandle(), ownsHandle: false);
    }
}

file static class ConsoleOpenStandardOutputHandle
{
    public static IntPtr GetHandle() => Environment.OSVersion.Platform == PlatformID.Win32NT ? GetStdHandle(-11) : new IntPtr(1);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetStdHandle(int handleType);
}
