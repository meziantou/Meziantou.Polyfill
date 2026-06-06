using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

static partial class PolyfillExtensions
{
    extension(Console)
    {
        public static SafeFileHandle OpenStandardInputHandle() => new(ConsoleOpenStandardInputHandle.GetHandle(), ownsHandle: false);
    }
}

file static class ConsoleOpenStandardInputHandle
{
    public static IntPtr GetHandle() => Environment.OSVersion.Platform == PlatformID.Win32NT ? GetStdHandle(-10) : IntPtr.Zero;

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetStdHandle(int handleType);
}
