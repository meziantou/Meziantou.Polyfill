using System;
using System.IO;
static partial class PolyfillExtensions_Path
{
    extension(Path)
    {
        public static string Join(params string?[] paths)
        {
            if (paths == null)
                throw new ArgumentNullException(nameof(paths));

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < paths.Length; i++)
            {
                var path = paths[i];
                if (string.IsNullOrEmpty(path))
                    continue;

                if (sb.Length == 0)
                {
                    sb.Append(path);
                }
                else
                {
                    if (!IsDirectorySeparator(sb[sb.Length - 1]) && !IsDirectorySeparator(path[0]))
                        sb.Append(Path.DirectorySeparatorChar);

                    sb.Append(path);
                }
            }

            return sb.ToString();
        }

        private static bool IsDirectorySeparator(char c) => c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
    }
}
