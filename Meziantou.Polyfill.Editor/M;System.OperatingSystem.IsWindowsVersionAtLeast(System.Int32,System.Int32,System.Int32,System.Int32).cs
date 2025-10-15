using System;

static partial class PolyfillExtensions
{
    extension(OperatingSystem)
    {
        public static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            if (!IsWindows())
                return false;

            Version current = Environment.OSVersion.Version;
            if (current.Major != major)
                return current.Major > major;

            if (current.Minor != minor)
                return current.Minor > minor;

            // Unspecified build component is to be treated as zero
            var currentBuild = current.Build < 0 ? 0 : current.Build;
            build = build < 0 ? 0 : build;
            if (currentBuild != build)
                return currentBuild > build;

            // Unspecified revision component is to be treated as zero
            var currentRevision = current.Revision < 0 ? 0 : current.Revision;
            revision = revision < 0 ? 0 : revision;

            return currentRevision >= revision;
        }
    }
}
