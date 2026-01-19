// XML-DOC: M:System.IO.File.WriteAllBytesAsync(System.String,System.Byte[],System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER && !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System.IO;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static async Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default)
        {
            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
            await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
        }
    }
}
#endif
