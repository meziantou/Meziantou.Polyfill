using System;

static partial class PolyfillExtensions
{
    extension(OperatingSystem)
    {
        public static bool IsWindows()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }
    }
}