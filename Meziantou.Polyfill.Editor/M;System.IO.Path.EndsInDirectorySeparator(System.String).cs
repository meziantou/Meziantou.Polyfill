using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static bool EndsInDirectorySeparator(string path) => path is not null && path.Length > 0 && (path[path.Length - 1] == Path.DirectorySeparatorChar || path[path.Length - 1] == Path.AltDirectorySeparatorChar);
    }
}
