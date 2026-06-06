using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static string Join(params string?[] paths) => string.Join(Path.DirectorySeparatorChar.ToString(), paths);
    }
}
