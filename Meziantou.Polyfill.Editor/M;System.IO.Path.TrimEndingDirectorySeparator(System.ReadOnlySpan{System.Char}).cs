using System;
using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char> path) => EndsInDirectorySeparator(path) && Path.GetPathRoot(path.ToString())?.Length != path.Length ? path[..^1] : path;
    }
}
