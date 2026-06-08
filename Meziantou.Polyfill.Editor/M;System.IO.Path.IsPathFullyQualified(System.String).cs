using System;
using System.IO;

static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static bool IsPathFullyQualified(string path)
        {
            bool IsDirectorySeparator(char c) => c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;

            bool IsValidDriveChar(char value)
                => (uint)((value | 0x20) - 'a') <= (uint)('z' - 'a');

            if (path is null)
                throw new ArgumentNullException(nameof(path));

            if (Path.DirectorySeparatorChar == '\\')
            {
                if (path.Length < 2)
                    return false;

                if (IsDirectorySeparator(path[0]))
                    return path[1] == '?' || IsDirectorySeparator(path[1]);

                return path.Length >= 3
                    && path[1] == ':'
                    && IsDirectorySeparator(path[2])
                    && IsValidDriveChar(path[0]);
            }

            return path.Length > 0 && path[0] == '/';
        }
    }
}