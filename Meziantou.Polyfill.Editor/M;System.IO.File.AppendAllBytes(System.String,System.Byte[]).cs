// XML-DOC: M:System.IO.File.AppendAllBytes(System.String,System.Byte[])
#if !NET10_0_OR_GREATER
using System.IO;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static void AppendAllBytes(string path, byte[] bytes)
        {
            using var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
#endif
