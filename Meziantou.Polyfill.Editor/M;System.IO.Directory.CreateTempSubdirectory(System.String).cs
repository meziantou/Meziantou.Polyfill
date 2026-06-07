#pragma warning disable CA1510

using System;
using System.IO;

static partial class PolyfillExtensions_Directory
{
    extension(Directory)
    {
        public static DirectoryInfo CreateTempSubdirectory(string? prefix = null)
        {
            if (prefix is not null && prefix.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException("The prefix contains invalid characters.", nameof(prefix));

            while (true)
            {
                var path = Path.Combine(Path.GetTempPath(), prefix + Path.GetRandomFileName());
                if (!Directory.Exists(path) && !File.Exists(path))
                    return Directory.CreateDirectory(path);
            }
        }
    }
}
