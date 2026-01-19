// XML-DOC: M:System.IO.File.WriteAllTextAsync(System.String,System.String,System.Text.Encoding,System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER && !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static async Task WriteAllTextAsync(string path, string? contents, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (contents == null)
            {
                using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
                return;
            }

            using var stream2 = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
            using var writer = new StreamWriter(stream2, encoding);
            await writer.WriteAsync(contents).ConfigureAwait(false);
        }
    }
}
#endif
