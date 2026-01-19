// XML-DOC: M:System.IO.File.WriteAllText(System.String,System.ReadOnlySpan{System.Char})
#if !NET10_0_OR_GREATER
using System;
using System.IO;
using System.Text;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static void WriteAllText(string path, ReadOnlySpan<char> contents)
        {
            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read);
            using var writer = new StreamWriter(stream, Encoding.UTF8);
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            writer.Write(contents);
#else
            writer.Write(contents.ToString());
#endif
        }
    }
}
#endif
