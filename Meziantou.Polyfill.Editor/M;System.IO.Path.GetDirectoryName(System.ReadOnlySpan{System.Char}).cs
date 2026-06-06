using System;
using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char> path)
        {
            var fileName = Path.GetFileName(path);
            return fileName.Length == path.Length ? default : path[..(path.Length - fileName.Length - 1)];
        }
    }
}
