using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static bool Exists(string? path) => !string.IsNullOrEmpty(path) && (File.Exists(path) || Directory.Exists(path));
    }
}
