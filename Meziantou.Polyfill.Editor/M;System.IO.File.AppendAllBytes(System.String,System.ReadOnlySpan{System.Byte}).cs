// XML-DOC: M:System.IO.File.AppendAllBytes(System.String,System.ReadOnlySpan{System.Byte})
#if !NET10_0_OR_GREATER
using System;
using System.IO;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static void AppendAllBytes(string path, ReadOnlySpan<byte> bytes)
        {
            using var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read);
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            stream.Write(bytes);
#else
            var buffer = bytes.ToArray();
            stream.Write(buffer, 0, buffer.Length);
#endif
        }
    }
}
#endif
