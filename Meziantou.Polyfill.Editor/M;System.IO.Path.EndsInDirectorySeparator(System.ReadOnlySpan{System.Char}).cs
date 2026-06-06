using System;
using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static bool EndsInDirectorySeparator(ReadOnlySpan<char> path) => path.Length > 0 && (path[^1] == Path.DirectorySeparatorChar || path[^1] == Path.AltDirectorySeparatorChar);
    }
}
