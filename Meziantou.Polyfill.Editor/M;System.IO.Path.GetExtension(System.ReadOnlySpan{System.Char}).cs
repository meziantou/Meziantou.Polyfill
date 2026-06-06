using System;
using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static ReadOnlySpan<char> GetExtension(ReadOnlySpan<char> path)
        {
            var fileName = Path.GetFileName(path);
            var index = fileName.LastIndexOf('.');
            return index < 0 ? default : fileName[index..];
        }
    }
}
