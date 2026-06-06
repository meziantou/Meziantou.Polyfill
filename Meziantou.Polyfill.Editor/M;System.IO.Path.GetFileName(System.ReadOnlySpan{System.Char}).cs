// when M:System.IO.Path.GetDirectoryName(System.ReadOnlySpan{System.Char})
// when M:System.IO.Path.GetExtension(System.ReadOnlySpan{System.Char})
// when M:System.IO.Path.GetFileNameWithoutExtension(System.ReadOnlySpan{System.Char})
// when M:System.IO.Path.HasExtension(System.ReadOnlySpan{System.Char})
// when M:System.IO.Path.TrimEndingDirectorySeparator(System.ReadOnlySpan{System.Char})
using System;
using System.IO;

static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path) => PathPolyfillHelpers.GetFileName(path);
    }
}

file static class PathPolyfillHelpers
{
    public static ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path)
    {
        var index = path.LastIndexOfAny(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        return path[(index + 1)..];
    }
}
