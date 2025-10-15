using System;

static partial class PolyfillExtensions
{
    extension(OperatingSystem)
    {
        public static bool IsWindows()
        {
#if NET || NETSTANDARD2_0 || NET471_OR_GREATER
            return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
#else
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
#endif
        }
    }
}