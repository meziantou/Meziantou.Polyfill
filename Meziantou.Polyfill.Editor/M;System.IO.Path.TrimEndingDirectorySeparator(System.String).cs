using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static string TrimEndingDirectorySeparator(string path) => path.Length > 0 && (path[path.Length - 1] == Path.DirectorySeparatorChar || path[path.Length - 1] == Path.AltDirectorySeparatorChar) && path != Path.GetPathRoot(path) ? path.Substring(0, path.Length - 1) : path;
    }
}
