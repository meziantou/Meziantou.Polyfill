// XML-DOC: M:System.IO.File.ReadAllBytesAsync(System.String,System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER && !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System.IO;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static async Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = default)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1, useAsync: true);
            var length = stream.Length;
            if (length > int.MaxValue)
            {
                throw new IOException("File too large");
            }

            var bytes = new byte[(int)length];
            var offset = 0;
            var remaining = (int)length;
            while (remaining > 0)
            {
                var read = await stream.ReadAsync(bytes, offset, remaining, cancellationToken).ConfigureAwait(false);
                if (read == 0)
                    break;

                offset += read;
                remaining -= read;
            }

            return bytes;
        }
    }
}
#endif
