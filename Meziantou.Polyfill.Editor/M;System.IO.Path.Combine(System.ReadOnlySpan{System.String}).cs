using System;
using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static string Combine(ReadOnlySpan<string> paths) => Path.Combine(paths.ToArray());
    }
}
