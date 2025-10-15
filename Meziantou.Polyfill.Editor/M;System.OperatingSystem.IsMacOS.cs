using System;

static partial class PolyfillExtensions
{
    extension(OperatingSystem)
    {
        public static bool IsMacOS()
        {
            return Environment.OSVersion.Platform == PlatformID.MacOSX;
        }
    }
}