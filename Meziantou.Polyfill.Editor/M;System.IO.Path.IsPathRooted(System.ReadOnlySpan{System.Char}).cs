using System;
using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static bool IsPathRooted(ReadOnlySpan<char> path) => Path.IsPathRooted(path.ToString());
    }
}
