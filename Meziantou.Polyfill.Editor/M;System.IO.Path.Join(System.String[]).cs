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
                    if (sb[sb.Length - 1] != Path.DirectorySeparatorChar &&
                        sb[sb.Length - 1] != Path.AltDirectorySeparatorChar &&
                        path[0] != Path.DirectorySeparatorChar &&
                        path[0] != Path.AltDirectorySeparatorChar)
                        sb.Append(Path.DirectorySeparatorChar);

                    sb.Append(path);
                }
            }

            return sb.ToString();
        }
    }
}
