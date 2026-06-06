using System;
using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static bool HasExtension(ReadOnlySpan<char> path)
        {
            var fileName = Path.GetFileName(path);
            return fileName.LastIndexOf('.') >= 0;
        }
    }
}
