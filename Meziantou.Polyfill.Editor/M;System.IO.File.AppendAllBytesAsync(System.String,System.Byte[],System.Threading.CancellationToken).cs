// XML-DOC: M:System.IO.File.AppendAllBytesAsync(System.String,System.Byte[],System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER
using System.IO;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static async Task AppendAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default)
        {
#if NET7_0_OR_GREATER
            await using var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
#else
            using var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
#endif
            await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
        }
    }
}
#endif
